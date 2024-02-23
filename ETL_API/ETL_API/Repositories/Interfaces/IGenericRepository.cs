using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ETL_API.Repositories.Interfaces
{ 
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<K?> ExecuteSqlSingleAsync<K>(string sql) where K : class, new();
        Task<IEnumerable<K>> ExecuteSqlCollectionAsysnc<K>(string sql) where K : class, new();
        Task ExecuteCommandAsync(string sql);
    }
}
