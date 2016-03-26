namespace SampleApp.Data.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserLogins")]
    public class UserLogin : IdentityUserLogin<string>
    {
    }
}