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

    public class RoleRepository :
        RoleStore<Role, string, UserRole>,
        IQueryableRoleStore<Role, string>,
        IRoleStore<Role, string>,
        IRoleRepository

    {
        private ILoggerService log = null;
        public RoleRepository()
            : this(new SampleAppContext(), new LoggerService())
        {
            DisposeContext = true;
        }
        public RoleRepository(DbContext context, ILoggerService logger)
            : base(context)
        {
            log = logger;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = await Roles.ToListAsync();
                await Task.FromResult(0);
                log.TraceApi("SQL Database", "RoleRepository.GetAllAsync", timespan.Elapsed, "Type={0}", GetType());
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
                var result = await Roles.CountAsync();
                await Task.FromResult(0);
                log.TraceApi("SQL Database", "RoleRepository.GetAllAsync", timespan.Elapsed, "Type={0}", GetType());
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