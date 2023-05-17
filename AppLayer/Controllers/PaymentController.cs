using BLL.DTOs.Payment;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AppLayer.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PaymentController : ApiController
    {
        [HttpPost]
        [Route("api/payment/create")]
        public HttpResponseMessage Create(PaymentDTO payment)
        {
            try
            {
                var res = PaymentService.Create(payment);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/payments")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = PaymentService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/payments/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = PaymentService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
        }
        [HttpPut]
        [Route("api/payment/update")]
        public HttpResponseMessage Update(PaymentDTO payment)
        {
            var dbCustomer = PaymentService.Get(payment.Id);
            if (dbCustomer != null)
            {
                try
                {
                    PaymentService.Update(payment);
                    return Request.CreateResponse(HttpStatusCode.OK, payment);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Payment does not exist" });
            }
        }
        [HttpDelete]
        [Route("api/payment/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var dbPayment = PaymentService.Get(id);
            if (dbPayment != null)
            {
                try
                {
                    var res = PaymentService.Delete(id);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Payment does not exist" });
            }
        }
    }
}
