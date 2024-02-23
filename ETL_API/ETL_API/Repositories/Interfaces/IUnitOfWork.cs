namespace ETL_API.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IGenericRepository<T> GetRepository<T>() where T : class, new();
        Task<bool> SaveAsync();
    }
}
