using BLL.DTOs;
using BLL.DTOs.Customer;
using BLL.DTOs.Seller;
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

namespace AppLayer.Controllers
{
    public class SellerController : ApiController
    {
        [HttpPost]
        [Route("api/seller/create")]
        public HttpResponseMessage Create(SellerCreateDTO seller)
        {
            var dbSeller = SellerService.GetByEmail(seller.Email);
            if (dbSeller != null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Email has already taken. Please login");
            }
            try
            {
                var res = SellerService.Create(seller);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/sellers")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = SellerService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/sellers/{guid}")]
        public HttpResponseMessage GetByGuid(string guid)
        {
            try
            {
                var data = SellerService.Get(guid);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("api/seller/update")]
        public HttpResponseMessage Update(SellerDTO seller)
        {
            var dbCustomer = SellerService.Get(seller.Guid);
            if (dbCustomer != null)
            {
                try
                {
                    SellerService.Update(seller);
                    return Request.CreateResponse(HttpStatusCode.OK, seller);
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
        [HttpDelete]
        [Route("api/seller/delete/{guid}")]
        public HttpResponseMessage Delete(string guid)
        {
            var dbSeller = SellerService.Get(guid);
            if (dbSeller != null)
            {
                try
                {
                    SellerService.Delete(guid);
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
        // Seller + Products
        [HttpGet]
        [Route("api/sellers/{guid}/products")]
        public HttpResponseMessage GetWithProducts(string guid)
        {
            try
            {
                var data = SellerService.GetWithProducts(guid);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        // Others
        [HttpPost]
        [Route("api/seller/photo/upload/{guid}")]
        public HttpResponseMessage UploadPhoto(string guid)
        {
            var dbUser = SellerService.Get(guid);
            if (dbUser != null)
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    try
                    {
                        if (dbUser.Photo != null)
                        {
                            FileHandle.DeletePhoto("/SellerPhotos/", dbUser.Photo);
                        }
                        string photoName = FileHandle.SellerUploadPhoto(httpRequest, dbUser.Id);
                        var res = SellerService.UploadPhoto(guid, photoName);
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
        [Route("api/seller/photo/{guid}")]
        public HttpResponseMessage GetPhoto(string guid)
        {
            var user = SellerService.Get(guid);
            if (user != null)
            {
                if (user.Photo != null)
                {
                    var rootPath = HttpContext.Current.Server.MapPath("/Uploads/SellerPhotos/");
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
        [Route("api/seller/photo/delete/{guid}")]
        public HttpResponseMessage DeletePhoto(string guid)
        {
            var user = SellerService.Get(guid);
            if (user != null)
            {
                if (user.Photo != null)
                {
                    try
                    {
                        bool isDeleted = FileHandle.DeletePhoto("/SellerPhotos/", user.Photo);
                        var res = SellerService.DeletePhoto(guid);
                        return Request.CreateResponse(HttpStatusCode.OK, res);
                    }
                    catch(Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [HttpPatch]
        [Route("api/seller/change-password/{guid}")]
        public HttpResponseMessage ChangePassword(string guid, ChangePassDTO changePass)
        {
            var dbSeller = SellerService.Get(guid);
            if (dbSeller != null)
            {
                try
                {
                    var res = SellerService.ChangePassword(guid, changePass);
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
