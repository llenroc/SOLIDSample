namespace SampleApp.Data.Repositories
{
    using Core.Interfaces;
    using Identity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class UserRepository :
        UserStore<User, Role, string, UserLogin, UserRole, UserClaim>,
        IQueryableUserStore<User, string>,
        IUserStore<User, string>,
        IUserRepository
    {
        private ILoggerService log = null;
        public UserRepository()
            : this(new SampleAppContext(), new LoggerService())
        {
            DisposeContext = true;
        }

        public UserRepository(DbContext context, ILoggerService logger)
            : base(context)
        {
            log = logger;
        }

        public async Task<List<User>> GetAllAsync()
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = await Users.ToListAsync();
                await Task.FromResult(result);
                log.TraceApi("SQL Database", "UserRepository.GetAllAsync()", timespan.Elapsed, "Type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in {0}", GetType());
                throw;
            }
        }

        public async Task<int> TotalAsync()
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = await Users.CountAsync();
                await Task.FromResult(result);
                timespan.Stop();
                log.TraceApi("SQL Database", GetType().ToString() + ".TotalAsync()", timespan.Elapsed, "Type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in {0}", GetType());
                throw;
            }
        }
    }
}