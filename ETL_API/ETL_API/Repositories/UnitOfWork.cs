using ETL_API.Repositories.Interfaces;
using ETL_Shared.Models; 

namespace ETL_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PatientDbContext db;
        public UnitOfWork(PatientDbContext db)
        {
            this.db = db;
        }
        public IGenericRepository<T> GetRepository<T>() where T : class, new()
        {
            return new GenericRepository<T>(db);
        }
        public async Task<bool> SaveAsync()
        {
            int rowsEffected = await db.SaveChangesAsync();
            return rowsEffected > 0;
        }
    }
}
