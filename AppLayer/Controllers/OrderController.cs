using BLL.DTOs.Order;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppLayer.Controllers
{
    public class OrderController : ApiController
    {
        [HttpPost]
        [Route("api/order/create")]
        public HttpResponseMessage Create(OrderDTO order)
        {
            try
            {
                var res = OrderService.Create(order);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/orders")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = OrderService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/orders/{code}")]
        public HttpResponseMessage GetByGuid(string code)
        {
            try
            {
                var data = OrderService.Get(code);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        [Route("api/order/delete/{code}")]
        public HttpResponseMessage Delete(string code)
        {
            var dbSeller = OrderService.Get(code);
            if (dbSeller != null)
            {
                try
                {
                    var res = OrderService.Delete(code);
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
        [HttpPut]
        [Route("api/order/{code}/process")]
        public HttpResponseMessage ProcessOrder(string code)
        {
            var data = OrderService.Get(code);
            if (data != null)
            {
                try
                {
                    var res = OrderService.ProcessOrder(code);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [HttpPut]
        [Route("api/order/{code}/deliver")]
        public HttpResponseMessage DeliverOrder(string code)
        {
            var data = OrderService.Get(code);
            if (data != null)
            {
                try
                {
                    var res = OrderService.DeliverOrder(code);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
