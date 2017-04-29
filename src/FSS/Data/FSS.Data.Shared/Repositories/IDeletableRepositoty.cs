namespace FSS.Data.Shared.Repositories
{
    using FSS.Data.Shared.Models;
    using System.Linq;

    public interface IDeletableRepositoty<T> : IRepository<T>
        where T : class, IDeletableEntity
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);

        void RecoverDeleted(T entity);
    }
}
