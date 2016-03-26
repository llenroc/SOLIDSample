namespace SampleApp.Data.Migrations
{
    using Core.Models;
    using Identity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repositories;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SampleAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SampleAppContext context)
        {
            InitializeDb(context);

            context.Leads.AddOrUpdate(l => l.FirstName,
                new MarketingLead { FirstName = "Marketing", LastName = "Lead1", Email = "someemail1@mail.com"  }
                , new MarketingLead { FirstName = "Marketer", LastName = "lead2", Email = "someemail2@mail.com" }
                , new MarketingLead { FirstName = "Market", LastName = "Lead3", Email = "someemail3@mail.com" });
        }

        private void InitializeDb(SampleAppContext context)
        {
            var UserManager = new UserManager<User, string>(new UserStore<User, Role, string, UserLogin, UserRole, UserClaim>(new SampleAppContext()));
            var RoleManager = new RoleManager<Role, string>(new RoleStore<Role, string, UserRole>(new SampleAppContext()));
            var ProfileManager = new ProfileRepository(context, new LoggerService());
            DateTime now = DateTime.UtcNow;

            #region Create Application Roles
            List<string> roles = new List<string>();
            roles.Add("Administrator");
            roles.Add("User");

            foreach (string role in roles)
            {
                if (RoleManager.FindByName(role) == null)
                {
                    var newRole = new Role
                    {
                        Name = role,
                    };
                    RoleManager.Create(newRole);
                }
            }
            #endregion

            #region Create Admin Super User

            const string name = "sampleadmin@sampleapp.com";
            const string password = "admin1234";
            string adminId = Guid.NewGuid().ToString();

            var adminUser = UserManager.FindByName(name);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    Id = adminId,
                    Email = name,
                    UserName = name,
                    PhoneNumber = "(012) 345-6789",
                    PhoneNumberConfirmed = true,
                    AllowEdit = false,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    CreatedDate = now
                };
                var result = UserManager.Create(adminUser, password);
                result = UserManager.SetLockoutEnabled(adminUser.Id, false);
            }

            var userRoles = UserManager.GetRoles(adminUser.Id);
            if (!userRoles.Contains(roles[0]))
            {
                var result = UserManager.AddToRole(adminUser.Id, roles[0]);
            }

            var adminProfile = ProfileManager.GetById(name);
            if(adminProfile == null)
            {
                adminProfile = new Profile
                {
                    ProfileId = name,
                    UpdatedBy = name,
                    UpdatedDate = now,
                    CreatedBy = name,
                    CreatedDate = now,
                    FirstName = "Sample",
                    LastName = "Administrator",
                };
                ProfileManager.Create(adminProfile);
            }

            #endregion
        }
    }
}
