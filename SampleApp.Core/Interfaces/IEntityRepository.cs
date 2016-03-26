namespace SampleApp.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IEntityRepository<T> where T : class
    {
        Task<bool> ExistsAsync(object primaryKey);
        Task<int> TotalAsync();
        Task CreateAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<T> GetByIdAsync(object id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> where);
        Task Update(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> where);
        Task DeleteAsync(object id);
    }
}
