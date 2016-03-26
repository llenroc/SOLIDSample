namespace SampleApp.Data.Repositories
{
    using Core.Interfaces;
    using Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProfileRepository : EntityRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(SampleAppContext dbContext, ILoggerService logger)
            :base(dbContext, logger)
        {

        }

        public IList<Profile> ListAllByProfileType(int pageNo, int pageSize, string profileType)
        {
            throw new NotImplementedException();
        }

        public IList<Profile> ListAllProfiles()
        {
            throw new NotImplementedException();
        }

        public Profile Profile(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Profile> ProfileAsync(string id)
        {
            throw new NotImplementedException();
        }

        public int TotalProfiles()
        {
            throw new NotImplementedException();
        }

        public int TotalPublishedProfiles(bool isPublished = true)
        {
            throw new NotImplementedException();
        }
    }
}