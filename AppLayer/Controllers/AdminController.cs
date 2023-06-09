﻿using BLL.DTOs;
using BLL.DTOs.Admin;
using BLL.DTOs.Customer;
using BLL.Helpers;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AppLayer.Controllers
{
    [EnableCors("*", "*", "*")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/admin/photo/upload/{guid}")]
        public HttpResponseMessage FileUpload(string guid)
        {
            var dbUser = AdminService.Get(guid);
            if (dbUser != null)
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    try
                    {
                        if (dbUser.Photo != null)
                        {
                            FileHandle.DeletePhoto("/AdminPhotos/", dbUser.Photo);
                        }
                        string photoName = FileHandle.AdminUploadPhoto(httpRequest, dbUser.Id);
                        var res = AdminService.UploadPhoto(guid, photoName);
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/admin/photo/{guid}")]
        public HttpResponseMessage GetPhoto(string guid)
        {
            var user = AdminService.Get(guid);
            if (user != null)
            {
                if (user.Photo != null)
                {
                    var rootPath = HttpContext.Current.Server.MapPath("/Uploads/AdminPhotos/");
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
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/admin/photo/delete/{guid}")]
        public HttpResponseMessage DeletePhoto(string guid)
        {
            var user = AdminService.Get(guid);
            if (user.Photo != null)
            {
                try
                {
                    bool isDeleted = FileHandle.DeletePhoto("/AdminPhotos/", user.Photo);
                    var res = AdminService.DeletePhoto(guid);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPatch]
        [Route("api/admin/change-email/{guid}")]
        public HttpResponseMessage ChangeEmail(string guid, ChangeEmailDTO changeEmail)
        {
            var dbUser = AdminService.Get(guid);
            if (dbUser != null)
            {
                var existEmail = AdminService.GetByEmail(changeEmail.Email);
                if (existEmail != null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Email already taken" });
                }
                try
                {
                    var res = AdminService.ChangeEmail(guid, changeEmail);
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
