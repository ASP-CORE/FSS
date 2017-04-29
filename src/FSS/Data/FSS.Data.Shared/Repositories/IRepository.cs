namespace FSS.Data.Shared.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> All();

        Task<T> GetByIdAsync(object id);

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
