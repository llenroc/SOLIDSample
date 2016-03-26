namespace SampleApp.Core.Models.Abstracts
{
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class Person : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string PublicName { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string DisplayName { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string LastNameFirst { get; private set; }
    }
}