namespace SampleApp.Data.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserClaims")]
    public class UserClaim : IdentityUserClaim<string>
    {
    }
}