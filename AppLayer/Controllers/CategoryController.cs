using BLL.DTOs.Category;
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
    public class CategoryController : ApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/category/create")]
        public HttpResponseMessage Create(CategoryDTO category)
        {
            try
            {
                var res = CategoryService.Create(category);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/categories")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = CategoryService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/categories/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = CategoryService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/category/update")]
        public HttpResponseMessage Update(CategoryDTO category)
        {
            var dbCategory = CategoryService.Get(category.Id);
            if (dbCategory != null)
            {
                try
                {
                    var res = CategoryService.Update(category);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Category does not exist" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/category/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var dbCategory = CategoryService.Get(id);
            if (dbCategory != null)
            {
                try
                {
                    var res = CategoryService.Delete(id);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Category does not exist" });
            }
        }
        [HttpGet]
        [Route("api/categories/{id}/products")]
        public HttpResponseMessage GetWithProducts(int id)
        {
            try
            {
                var data = CategoryService.GetWithProducts(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
        }
    }
}
