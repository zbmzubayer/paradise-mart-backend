using AppLayer.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
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
        // Extract user info from JWT token
        [HttpGet]
        [Authorize]
        [Route("api/auth/login/info")]
        public HttpResponseMessage GetCurrentUser()
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var id = userClaims.FirstOrDefault(O => O.Type == ClaimTypes.NameIdentifier)?.Value;
                var email = userClaims.FirstOrDefault(O => O.Type == ClaimTypes.Email)?.Value;
                var role = userClaims.FirstOrDefault(O => O.Type == ClaimTypes.Role)?.Value;
                return Request.CreateResponse(HttpStatusCode.OK, new { Id = id, Email = email, Role = role });
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
