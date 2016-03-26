namespace SampleApp.Data.Repositories
{
    using Core.Interfaces;
    using Core.Models;

    public class MarketingLeadRepository : EntityRepository<MarketingLead>,IMarketingLeadRepository
    {
        public MarketingLeadRepository(SampleAppContext dbContext, ILoggerService logger)
            :base(dbContext,logger)
        { }
    }
}