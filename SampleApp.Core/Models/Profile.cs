namespace SampleApp.Core.Models
{
    using Abstracts;
    using System.ComponentModel.DataAnnotations;

    public class Profile : Person
    {
        [Key/*, ForeignKey("User")*/]
        public string ProfileId { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string ProfilePictureUrl { get; set; }
        //public User User { get; set; }

        public string Slug { get; set; }
    }
}