using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(MyBudget.Startup))]
namespace MyBudget
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //ConfigureOAuthTokenConsumption(app);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = "z";//ConfigurationManager.AppSettings["Issuer"];
            var audienceId = "z";// ConfigurationManager.AppSettings["AudienceId"];
            var audienceSecret = "z";// TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters()
                {
                    AuthenticationType = "Bearer",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JUSTDOITJUSTDOITJUSTDOIT"))
                }

            });
        }
    }
}
