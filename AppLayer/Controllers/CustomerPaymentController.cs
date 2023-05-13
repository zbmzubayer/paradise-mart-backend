using BLL.DTOs.CustomerPayment;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppLayer.Controllers
{
    public class CustomerPaymentController : ApiController
    {
        [HttpPost]
        [Route("api/customer-payment/create")]
        public HttpResponseMessage Create(CustomerPaymentDTO customerPayment)
        {
            try
            {
                var res = CustomerPaymentService.Create(customerPayment);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/customer-payments")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = CustomerPaymentService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/customer-payments/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = CustomerPaymentService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
        }
        [HttpPut]
        [Route("api/customer-payment/update")]
        public HttpResponseMessage Update(CustomerPaymentDTO customerPayment)
        {
            var dbCustomerPayment = CustomerPaymentService.Get(customerPayment.Id);
            if (dbCustomerPayment != null)
            {
                try
                {
                    var res = CustomerPaymentService.Update(customerPayment);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "CustomerPayment does not exist" });
            }
        }
        [HttpDelete]
        [Route("api/customer-payment/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var dbCustomerPayment = CustomerPaymentService.Get(id);
            if (dbCustomerPayment != null)
            {
                try
                {
                    var res = CustomerPaymentService.Delete(id);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "CustomerPayment does not exist" });
            }
        }
    }
}
