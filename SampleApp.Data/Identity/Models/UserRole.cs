namespace SampleApp.Data.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRoles")]
    public class UserRole : IdentityUserRole<string>
    {
    }
}