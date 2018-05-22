using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;


[assembly: OwinStartup(typeof(demo3.Controllers.Startup))]

namespace demo3.Controllers
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            var options = new OpenIdConnectAuthenticationOptions
            {
//#if DEBUG
                ClientId = "measure_spec",
//#else
//                    ClientId = "mpog.suite.web",
//#endif
                Authority = "https://www.aspirecqi.org/HIEBus/IdentityServer",
                ResponseType = "id_token",
                Scope = "openid profile roles email",

                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var token = n.ProtocolMessage.AccessToken;

                        if (!string.IsNullOrEmpty(token))
                        {
                            n.AuthenticationTicket.Identity.AddClaim(
                                new Claim("access_token", token));
                        }
                        await Task.Run(() => { }).ConfigureAwait(false);
                    },
                    RedirectToIdentityProvider = async n =>
                    {
                        var uri = n.Request.Uri; //to do to get the return uri
//#if DEBUG
                        n.ProtocolMessage.RedirectUri = "http://localhost:50683/Home/Login";
                        n.ProtocolMessage.PostLogoutRedirectUri = "http://localhost:50683/Measures/Index";
//#else
//                        n.ProtocolMessage.RedirectUri = "https://www.aspirecqi.org/AppSuite/";
//                        n.ProtocolMessage.PostLogoutRedirectUri = "https://www.aspirecqi.org/AppSuite/";
//#endif

                        await Task.Run(() => { }).ConfigureAwait(false);
                    },
                    //RedirectToIdentityProvider = n =>
                    //{
                    //    if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                    //    {
                    //        var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                    //        if (idTokenHint != null)
                    //        {
                    //            n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                    //        }
                    //    }
                    //    return Task.FromResult(0);
                    //},
                    AuthenticationFailed = (context) =>
                    {
                        if (context.Exception.Message.StartsWith("OICE_20004") || context.Exception.Message.Contains("IDX10311"))
                        {
                            context.SkipToNextMiddleware();
                            return Task.FromResult(0);
                        }
                        return Task.FromResult(0);
                    }
                }

                // original working code 
                //Notifications = new OpenIdConnectAuthenticationNotifications()
                //{
                //    RedirectToIdentityProvider = (context) =>
                //    {
                //        // Ensure the URI is picked up dynamically from the request;
                //        string appBaseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + context.Request.Uri.PathAndQuery;
                //        context.ProtocolMessage.RedirectUri = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + context.Request.Uri.PathAndQuery;
                //        context.ProtocolMessage.PostLogoutRedirectUri = appBaseUrl;
                //        return Task.FromResult(0);
                //    },
                //    AuthenticationFailed = (context) =>
                //    {
                //        if (context.Exception.Message.StartsWith("OICE_20004") || context.Exception.Message.Contains("IDX10311"))
                //        {
                //            context.SkipToNextMiddleware();
                //            return Task.FromResult(0);
                //        }
                //        return Task.FromResult(0);
                //    },
                //}
            };

            // clean up cookie
            //app.UseOidcNonceCookieCleanup(options);

            app.UseOpenIdConnectAuthentication(options);
        }

    }

}