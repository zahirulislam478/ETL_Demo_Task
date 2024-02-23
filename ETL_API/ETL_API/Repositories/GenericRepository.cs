using ETL_API.Repositories.Interfaces; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ETL_API.Repositories 
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        readonly DbContext db;
        readonly DbSet<T> dbSet;
        public GenericRepository(DbContext db)
        {
            this.db = db;
            this.dbSet = this.db.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> data = this.dbSet;
            if (includes != null)
            {
                data = includes(data);
            }
            return await data.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> data = this.dbSet;
            if (includes != null)
            {
                data = includes(data);
            }
            return await data.FirstOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await this.dbSet.FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public async Task<K?> ExecuteSqlSingleAsync<K>(string sql) where K : class, new()
        {
            FormattableString q = FormattableStringFactory.Create(sql);
            var data = await this.db.Set<K>()
                .FromSql(q)
                .FirstOrDefaultAsync();
            return data;
        }

        public async Task<IEnumerable<K>> ExecuteSqlCollectionAsysnc<K>(string sql) where K : class, new()
        {
            FormattableString q = FormattableStringFactory.Create(sql);
            var data = await this.db.Set<K>()
                .FromSql(q)
                .ToListAsync();
            return data;
        }

        public async Task ExecuteCommandAsync(string sql)
        {
            FormattableString q = FormattableStringFactory.Create(sql);
            await db.Database.ExecuteSqlInterpolatedAsync(q);
        }
    }
}
