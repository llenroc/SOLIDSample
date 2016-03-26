namespace SampleApp.Core.Interfaces
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProfileRepository : IEntityRepository<Profile>
    {
        Task<Profile> ProfileAsync(string id);

        Profile Profile(string id);

        IList<Profile> ListAllProfiles();
        IList<Profile> ListAllByProfileType(int pageNo, int pageSize, string profileType);
        int TotalPublishedProfiles(bool isPublished = true);
        int TotalProfiles();
    }
}
