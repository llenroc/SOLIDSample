namespace SampleApp.Data.Identity.Models
{
    using Core.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Table("User")]
    public class User : IdentityUser<string, UserLogin, UserRole, UserClaim>, IUser<string>, IAuditableEntity
    {
        public User()
        {
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public bool AllowEdit { get; set; }
        public DateTime? LastLogin { get; set; }
        public int LoginCount { get; set; }

        //[ForeignKey("Id")]
        //public Profile Profile { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}