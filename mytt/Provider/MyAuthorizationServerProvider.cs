using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mytt.Models;
using Microsoft.Owin.Security.OAuth;

namespace mytt.Provider
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                using (var db = new Contexto())
                {
                    var nomeUsuario = context.UserName;
                    var senha = Convert.ToBase64String(Encoding.UTF8.GetBytes(context.Password));

                    var usuario = await 
                        db.Usuario.FirstOrDefaultAsync(f =>
                            f.Username == nomeUsuario &&
                            f.Password == senha);

                    if (usuario != null && usuario != new Usuario())
                    {
                        var nomeUsuarioClaim = new Claim(ClaimTypes.Name, nomeUsuario);
                        var nameidentifier = new Claim(ClaimTypes.NameIdentifier, new Guid().ToString());
                        var identityProvider = new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", new Guid().ToString());

                        var claims = new List<Claim> { nomeUsuarioClaim, nameidentifier, identityProvider };

                        // não há necessidade de incluir permissões
                        // pois as permissões são as mesmas para todos
                        var identity = new ClaimsIdentity(claims, "myttApp");

                        var principal = new GenericPrincipal(identity, null);
                        Thread.CurrentPrincipal = principal;

                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Falha na autenticação!");
                    }
                }
            }
            catch
            {
                context.SetError("invalid_grant", "Falha na autenticação");
            }
        }
    }
}