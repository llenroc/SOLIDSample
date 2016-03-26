namespace SampleApp.Web
{
    using Data.Identity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using Services;
    using System;

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            /*
                Enable the application to use a cookie to store information for the signed in user
                and to use a cookie to temporarily store information about a user logging in with a third party login provider
                Configure the sign in cookie
            */
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                Provider = new CookieAuthenticationProvider
                {
                    /*
                        Enables the application to validate the security stamp when the user logs in.
                        This is a security feature which is used when you change a password or add an external login to your account.
                    */
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserService, User, string>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (id) => (id.GetUserId<string>()))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}