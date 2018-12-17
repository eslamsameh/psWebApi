using psBackEnd.Models;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace API_Server
{
    public class MyAuthinticationServerProvider : OAuthAuthorizationServerProvider
    {
        psDataBaseEntities db = new psDataBaseEntities();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           
            var user = db.admins.Where(x => x.userName == context.UserName && x.password == context.Password).First();
            try
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                if (user != null)
                {
                    identity.AddClaim(new Claim("adminId", user.adminID.ToString()));
                    identity.AddClaim(new Claim("role", user.role));
                    identity.AddClaim(new Claim("adminName", user.userName));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.userName));
                    context.Validated(identity);


                }

                else
                {
                    context.SetError("Invalid_grant", "Provider username and password is incorrect");
                }
            }
            catch
            {
                context.SetError( "Provider username and password is incorrect");
            }
           
        }
    }
}