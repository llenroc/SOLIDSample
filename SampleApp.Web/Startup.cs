using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SampleApp.Core.Interfaces;
using SampleApp.Data;
using SampleApp.Data.Repositories;
using SampleApp.Data.Services;
using SampleApp.Web.Services;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(SampleApp.Web.Startup))]

namespace SampleApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //  Register DB Context and Logging
            builder.RegisterType<SampleAppContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<LoggerService>().As<ILoggerService>().SingleInstance();

            #region AspNet.Identity IoC Registrations
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            //Register User Service
            builder.Register(c =>
                new UserService(
                    new UserRepository(
                        new SampleAppContext(), new LoggerService()), app.GetDataProtectionProvider()))
                        .InstancePerRequest();
            //Register Role Service
            builder.Register(c =>
                new RoleService(
                    new RoleRepository(
                        new SampleAppContext(), new LoggerService())))
                        .InstancePerRequest();
            //Register Sign In Service
            builder.Register(c =>
                new SignInService(
                    new UserService(
                        new UserRepository(
                            new SampleAppContext(), new LoggerService()), app.GetDataProtectionProvider()),
                            HttpContext.Current.GetOwinContext()
                            .Authentication))
                            .InstancePerRequest();
            #endregion

            //  Register Repositories
            #region Repository Registrations
            builder.RegisterAssemblyTypes(typeof(ProfileRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            #endregion

            //  Register services
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerRequest();

            //  Register Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);

            ConfigureAuth(app);
        }
    }
}
