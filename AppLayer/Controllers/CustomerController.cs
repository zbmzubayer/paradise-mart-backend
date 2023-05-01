using BLL.DTOs;
using BLL.DTOs.Customer;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.ApplicationServices;
using System.Web;
using System.Web.Http;
using BLL.Helpers;
using System.Net.Http.Headers;
using System.IO;

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
        [HttpPost]
        [Route("api/customer/photo/upload/{guid}")]
        public HttpResponseMessage FileUpload(string guid)
        {
            var dbUser = CustomerService.Get(guid);
            if(dbUser !=  null)
            {
                
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    try
                    {
                        if (dbUser.Photo != null)
                        {
                            FileHandle.DeletePhoto("/CustomerPhotos/", dbUser.Photo);
                        }
                        string photoName = FileHandle.CustomerUploadPhoto(httpRequest, dbUser.Id);
                        var res = CustomerService.UploadPhoto(guid, photoName);
                        return Request.CreateResponse(HttpStatusCode.Created, res);
                    }
                    catch(Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [HttpGet]
        [Route("api/customer/photo/{guid}")]
        public HttpResponseMessage GetPhoto(string guid)
        {
            var user = CustomerService.Get(guid);
            if(user != null)
            {
                if (user.Photo != null)
                {
                    var rootPath = HttpContext.Current.Server.MapPath("/Uploads/CustomerPhotos/");
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileFullPath = System.IO.Path.Combine(rootPath, user.Photo);
                    byte[] bfile = System.IO.File.ReadAllBytes(fileFullPath);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bfile);
                    response.Content = new ByteArrayContent(bfile);
                    // response.Content = new StreamContent(ms);
                    //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    //response.Content.Headers.ContentDisposition.FileName = file;
                    return response;
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [HttpDelete]
        [Route("api/customer/photo/delete/{guid}")]
        public HttpResponseMessage DeletePhoto(string guid)
        {
            var user = CustomerService.Get(guid);
            if (user != null)
            {
                if (user.Photo != null)
                {
                    try
                    {
                        bool isDeleted = FileHandle.DeletePhoto("/CustomerPhotos/", user.Photo);
                        var res = CustomerService.DeletePhoto(guid);
                        return Request.CreateResponse(HttpStatusCode.OK, res);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
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
