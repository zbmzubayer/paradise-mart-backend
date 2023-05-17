using AppLayer.Models;
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
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/auth/customer/login")]
        public HttpResponseMessage CustomerLogin(LoginModel login)
        {
            var user = AuthService.AuthenticateCustomer(login.Email, login.Password);
            if(user != null)
            {
                var token = TokenManager.GenerateToken(user.Guid, user.Email, "Customer");
                return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Invalid Credentials");
        }
        [HttpPost]
        [Route("api/auth/seller/login")]
        public HttpResponseMessage SellerLogin(LoginModel login)
        {
            var user = AuthService.AuthenticateSeller(login.Email, login.Password);
            if (user != null)
            {
                var token = TokenManager.GenerateToken(user.Guid, user.Email, "Seller");
                return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Invalid Credentials");
        }
        [HttpPost]
        [Route("api/auth/admin/login")]
        public HttpResponseMessage AdminLogin(LoginModel login)
        {
            var user = AuthService.AuthenticateAdmin(login.Email, login.Password);
            if (user != null)
            {
                var token = TokenManager.GenerateToken(user.Guid, user.Email, "Admin");
                return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Invalid Credentials");
        }
    }
}
