using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace demo3.Controllers
{ 
    public class OidcNonceCookieCleanupMiddleware : OwinMiddleware
    {
        /// <summary>
        /// OidcNonceCookieCleanupMiddleware cleans up nonce cookies which can be produced in large numbers by OpenIdConnectAuthenticatioMiddleware.
        /// Add OidcNonceCookieCleanupMiddleware prior to OpenIdConnectAuthenticatioMiddleware, and pass the same instance of OpenIdConnectAuthenticationOptions into
        /// both middlewares.
        /// </summary>
        /// <remarks>
        /// OpenIdConnectAuthenticatioMiddleware creates a new nonce cookie every time the login page appears.  The cookie is deleted after a successful login, 
        /// but cookies from abandoned login pages are not deleted until the browser is closed.  Over time these cookies can accululate until IIS rejects 
        /// requests because the http header is too large.
        /// 
        /// The method used here was inspired by code in the discussion at https://github.com/aspnet/Security/issues/179 which deletes all existing 
        /// nonce cookies whenever a new one is created.  That approach however does not support multiple login tabs, which is a common scenario at least
        /// in development.  The login will fail if the user attempts to login from a page for which the nonce cookie has been deleted.
        /// 
        /// Here we limit the number of concurrent nonce cookies to 5 (by default).  When that number is exceeded, to oldest one(s) are deleted.
        /// 
        /// This patch makes assumptions about implementation details in OpenIdConnectAuthenticationMiddleware, in 
        /// Microsoft.Owin.Security.OpenIdConnect, version 3.0.1.0.  A future version of that package may not be compatible, but will hopefully make 
        /// this patch no longer necessary.
        /// </remarks>
        public OidcNonceCookieCleanupMiddleware(OwinMiddleware next, OpenIdConnectAuthenticationOptions options, int maxNonceCookieCount) : base(next)
        {
            if (!typeof(OpenIdConnectAuthenticationMiddleware).Assembly.FullName.Contains("Version=3.0.1.0"))
            {
                throw new InvalidOperationException("OidcNonceCookieCleanupMiddleware was designed and tested for use with OpenIdConnectAuthenticationMiddleware v3.0.1.");
            }
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (!(options.ProtocolValidator?.RequireTimeStampInNonce ?? false))
            {
                throw new ArgumentException($"Unable to delete old nonce cookies because {nameof(options)}.{nameof(options.ProtocolValidator)}."
                + $"{nameof(options.ProtocolValidator.RequireTimeStampInNonce)} is false.", nameof(options));
            }
            if (maxNonceCookieCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxNonceCookieCount), "Value must not be less than one.");
            }
            Options = options;
            MaxNonceCookieCount = maxNonceCookieCount;
        }

        public override async Task Invoke(IOwinContext context)
        {
            context.Response.OnSendingHeaders(_ =>
            {
                if (!(context.Response.Headers["Set-Cookie"]?.Contains(NonceKeyPrefix) ?? false)) return;
                var cookiesToDelete = context.Request.Cookies
                    .Where(c => c.Key.StartsWith(NonceKeyPrefix) && IsBase64String(c.Value))
                    .Select(c => new
                    {
                        c.Key,
                        Options.StateDataFormat.Unprotect(Encoding.UTF8.GetString(Convert.FromBase64String(c.Value)))?.Dictionary
                           .FirstOrDefault(kvp => kvp.Key == NonceProperty).Value
                    })
                    .Where(x => (x?.Value?.Length ?? 0) > 18 && x.Value[18] == '.') // Ensure Unprotect succeeded and Value has expected timestamp prefix
                    .OrderByDescending(x => x.Value)
                    .Skip(MaxNonceCookieCount - 1)
                    .ToList();
                cookiesToDelete.ForEach(x => context.Response.Cookies.Delete(x.Key, new CookieOptions())); // Use CookieOptions to get the same Path value as original cookie
            }, null);

            await Next.Invoke(context);
        }

        public static readonly string NonceKeyPrefix = OpenIdConnectAuthenticationDefaults.CookiePrefix + "nonce";
        public static readonly string NonceProperty = "N";

        private OpenIdConnectAuthenticationOptions Options { get; }
        private int MaxNonceCookieCount { get; }

        private bool IsBase64String(string s) => (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$");
    }

    public static class OidcNonceCookieCleanupMiddlewareExtensions
    {
        public static IAppBuilder UseOidcNonceCookieCleanup(this IAppBuilder app, OpenIdConnectAuthenticationOptions openIdConnectOptions, int maxNonceCookieCount = 5)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (openIdConnectOptions == null) throw new ArgumentNullException(nameof(openIdConnectOptions));
            return app.Use(typeof(OidcNonceCookieCleanupMiddleware), openIdConnectOptions, maxNonceCookieCount);
        }
    }
}
