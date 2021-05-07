using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Configuration;

namespace TurismoRuralComunitario
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = "TURISMO",
                LogoutPath = new PathString("/Cuenta/CerrarSesion"),
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(15),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = (Microsoft.Owin.Security.AuthenticationMode)AuthenticationMode.None

            });
        }
    }
}