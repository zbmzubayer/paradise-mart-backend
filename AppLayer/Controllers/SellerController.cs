using BLL.DTOs;
using BLL.DTOs.Customer;
using BLL.DTOs.Seller;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
