using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(AppLayer.Startup))]

namespace AppLayer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseJwtBearerAuthentication(
                new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = ConfigurationManager.AppSettings["Jwt:Issuer"],
                        ValidAudience = ConfigurationManager.AppSettings["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Jwt:Secret"]))
                    }
                });
        }
    }
}
