using BLL.DTOs.Review;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppLayer.Controllers
{
    public class ReviewController : ApiController
    {
        [HttpPost]
        [Route("api/review/create")]
        public HttpResponseMessage Create(ReviewDTO review)
        {
            try
            {
                var res = ReviewService.Create(review);
                return Request.CreateResponse(HttpStatusCode.Created, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/reviews")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = ReviewService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/reviews/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = ReviewService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
        }
        [HttpPut]
        [Route("api/review/update")]
        public HttpResponseMessage Update(ReviewDTO review)
        {
            var dbReview = ReviewService.Get(review.Id);
            if (dbReview != null)
            {
                try
                {
                    ReviewService.Update(review);
                    return Request.CreateResponse(HttpStatusCode.OK, review);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Review does not exist" });
            }
        }
        [HttpDelete]
        [Route("api/review/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var dbReview = ReviewService.Get(id);
            if (dbReview != null)
            {
                try
                {
                    var res = ReviewService.Delete(id);
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Review does not exist" });
            }
        }
    }
}
