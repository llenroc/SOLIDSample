namespace SampleApp.Data.Repositories
{
    using Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        #region Declarations
        internal SampleAppContext Context;
        internal DbSet<T> DbEntitySet;
        private ILoggerService log = null;

        public EntityRepository(SampleAppContext dbContext, ILoggerService logger)
        {
            Context = dbContext;
            DbEntitySet = dbContext.Set<T>();
            log = logger;
            AutoSaveChanges = true;
        }

        public bool AutoSaveChanges { get; set; }

        public IQueryable<T> EntitySet
        {
            get { return DbEntitySet; }
        }

        #endregion

        public virtual async Task<bool> ExistsAsync(object primaryKey)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = DbEntitySet.Find(primaryKey) == null ? false : true;
                await Task.FromResult(result);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.Exists", timespan.Elapsed, "type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.Exists(id={0}) on Type, {1}", primaryKey, GetType());
                throw;
            }
        }

        public virtual async Task<int> TotalAsync()
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = DbEntitySet.Count();
                await Task.FromResult(result);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.Total", timespan.Elapsed, "type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.Total() on type, {0}", GetType());
                throw;
            }
        }

        public virtual void Create(T entity)
        {
            DbEntitySet.Add(entity);
        }

        public virtual async Task CreateAsync(T entity)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                DbEntitySet.Add(entity);
                await SaveChanges();
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.CreateAsync", timespan.Elapsed, "type={0}", GetType());
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.CreateAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = DbEntitySet.Where(where).FirstOrDefault<T>();
                await Task.FromResult(result);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.GetAsync", timespan.Elapsed, "type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.GetAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual T GetById(object id)
        {
            var result = DbEntitySet.Find(id);
            return result;
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = await DbEntitySet.FindAsync(id);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.GetByIdAsync", timespan.Elapsed, "type={0},params={1}", GetType(), id);
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.GetByIdAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = DbEntitySet.ToList();
                await Task.FromResult(result);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.GetAllAsync", timespan.Elapsed, "type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.GetAllAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var result = DbEntitySet.Where(where).ToList();
                await Task.FromResult(result);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.GetManyAsync", timespan.Elapsed, "type={0}", GetType());
                return result;
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.GetManyAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task Update(T entity)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                DbEntitySet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                await SaveChanges();
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.UpdateAsync", timespan.Elapsed, "type={0}", GetType());
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.UpdateAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                DbEntitySet.Remove(entity);
                await SaveChanges();
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.DeleteAsync", timespan.Elapsed, "type={0}", GetType());
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.DeleteAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                IEnumerable<T> objects = DbEntitySet.Where<T>(where).AsEnumerable();
                foreach (T obj in objects)
                {
                    DbEntitySet.Remove(obj);
                    await SaveChanges();
                }
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.DeleteAsync", timespan.Elapsed, "type={0}", GetType());
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.DeleteAsync on type, {0}", GetType());
                throw;
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            Stopwatch timespan = Stopwatch.StartNew();
            try
            {
                var entity = GetByIdAsync(id);
                if (entity == null) return; //not found; assume already deleted
                await DeleteAsync(entity);
                timespan.Stop();
                log.TraceApi("SQL Database", "EntityRepository.DeleteAsync", timespan.Elapsed, "type={0}", GetType());
            }
            catch (Exception e)
            {
                log.Error(e, "Error in EntityRepository.DeleteAsync on type, {0}", GetType());
                throw;
            }
        }

        // Only call save changes if AutoSaveChanges is true
        public virtual async Task SaveChanges()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }
    }
}