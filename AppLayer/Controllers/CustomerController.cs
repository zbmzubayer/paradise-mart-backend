using BLL.DTOs;
using BLL.DTOs.Customer;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppLayer.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpPost]
        [Route("api/customer/create")]
        public HttpResponseMessage Create(CustomerCreateDTO customer)
        {
            var dbCustomer = CustomerService.GetByEmail(customer.Email);
            if(dbCustomer != null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Email is already taken. Please login or use another email" });
            }
            try
            {
                var res = CustomerService.Create(customer);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/customers")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = CustomerService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/customers/{guid}")]
        public HttpResponseMessage GetByGuid(string guid)
        {
            try
            {
                var data = CustomerService.Get(guid);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("api/customer/update")]
        public HttpResponseMessage Update(CustomerDTO customer)
        {
            var dbCustomer = CustomerService.Get(customer.Guid);
            if(dbCustomer != null)
            {
                try
                {
                    CustomerService.Update(customer);
                    return Request.CreateResponse(HttpStatusCode.OK, customer);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "User Not found" });
            }
        }
        [HttpDelete]
        [Route("api/customer/delete/{guid}")]
        public HttpResponseMessage Delete(string guid)
        {
            var dbCustomer = CustomerService.Get(guid);
            if (dbCustomer != null)
            {
                try
                {
                    CustomerService.Delete(guid);
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        // Others
        [HttpPatch]
        [Route("api/customer/change-password/{guid}")]
        public HttpResponseMessage ChangePassword(string guid, ChangePassDTO changePass)
        {
            var dbCustomer = CustomerService.Get(guid);
            if (dbCustomer != null)
            {
                try
                {
                    var res = CustomerService.ChangePassword(guid, changePass);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("api/customer/forgot-password")]
        public HttpResponseMessage ForgotPassword(ForgotPassDTO forgotPass)
        {
            var dbCustomer = CustomerService.GetByEmail(forgotPass.Email);
            if (dbCustomer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Invalid Email" });
            }
            MailService.ForgotPassword(dbCustomer.Name, dbCustomer.Email);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
