namespace SampleApp.Web.Services
{
    using Data.Identity.Models;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    public class SignInService : SignInManager<User, string>
    {
        public SignInService(UserService userService, IAuthenticationManager authenticationManager)
            : base(userService, authenticationManager)
        { }
    }
}