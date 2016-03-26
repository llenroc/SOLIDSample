namespace SampleApp.Data.Identity.Models
{
    using Core.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Role")]
    public class Role : IdentityRole<string, UserRole>, IRole<string>, IAuditableEntity
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
        }

        public Role(string name)
        {
            Name = name;
            CreatedDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        public Role(string name, string description)
            : this(name)
        {
            Description = description;
        }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}