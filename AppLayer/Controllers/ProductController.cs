using BLL.DTOs.Customer;
using BLL.DTOs.Product;
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
    public class ProductController : ApiController
    {
        [HttpPost]
        [Route("api/product/create")]
        public HttpResponseMessage Create(ProductDTO product)
        {
            try
            {
                var res = ProductService.Create(product);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/products")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = ProductService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/products/{url}")]
        public HttpResponseMessage GetByUrl(string url)
        {
            try
            {
                var data = ProductService.Get(url);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPut]
        [Route("api/product/update")]
        public HttpResponseMessage Update(ProductDTO product)
        {
            var dbProduct = ProductService.Get(product.Url);
            if (dbProduct != null)
            {
                try
                {
                    ProductService.Update(product);
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Product Not found" });
            }
        }
        [HttpDelete]
        [Route("api/product/delete/{url}")]
        public HttpResponseMessage Delete(string url)
        {
            var dbCustomer = ProductService.Get(url);
            if (dbCustomer != null)
            {
                try
                {
                    ProductService.Delete(url);
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Product Not found" });
            }
        }
        [HttpGet]
        [Route("api/products/{url}/all")]
        public HttpResponseMessage GetAllInfo(string url)
        {
            try
            {
                var data = ProductService.GetAllInfo(url);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
