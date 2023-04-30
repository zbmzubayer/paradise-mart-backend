using BLL.DTOs;
using BLL.DTOs.Admin;
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
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("api/admin/create")]
        public HttpResponseMessage Create(AdminCreateDTO admin)
        {
            var dbAdmin = AdminService.GetByEmail(admin.Email);
            if (dbAdmin != null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Email is already taken. Please login or use another email" });
            }
            try
            {
                var res = AdminService.Create(admin);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/admins")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = AdminService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/admins/{guid}")]
        public HttpResponseMessage GetByGuid(string guid)
        {
            try
            {
                var data = AdminService.Get(guid);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("api/admin/update")]
        public HttpResponseMessage Update(AdminDTO admin)
        {
            var dbAdmin = AdminService.Get(admin.Guid);
            if (dbAdmin != null)
            {
                try
                {
                    AdminService.Update(admin);
                    return Request.CreateResponse(HttpStatusCode.OK, admin);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Admin not found" });
            }
        }
        [HttpDelete]
        [Route("api/admin/delete/{guid}")]
        public HttpResponseMessage Delete(string guid)
        {
            var dbCustomer = AdminService.Get(guid);
            if (dbCustomer != null)
            {
                try
                {
                    AdminService.Delete(guid);
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
        [Route("api/admin/change-password/{guid}")]
        public HttpResponseMessage ChangePassword(string guid, ChangePassDTO changePass)
        {
            var dbAdmin = AdminService.Get(guid);
            if (dbAdmin != null)
            {
                try
                {
                    var res = AdminService.ChangePassword(guid, changePass);
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
    }
}
