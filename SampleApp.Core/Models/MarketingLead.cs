namespace SampleApp.Core.Models
{
    using Abstracts;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MarketingLead : Person
    {
        public MarketingLead()
        {
            CreatedDate = DateTime.UtcNow;
            LeadId = Guid.NewGuid().ToString();
        }

        [Key]
        public string LeadId { get; set; }
        public string Email { get; set; }
        public string LeadNotes { get; set; }
    }
}