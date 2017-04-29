namespace FSS.Data.Repositories
{
    using System;
    using System.Linq;
    using FSS.Data.Shared.Models;
    using FSS.Data.Shared.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class EfDeletableRepository<TEntity> : EfRepository<TEntity>, IDeletableRepositoty<TEntity>
        where TEntity : class, IDeletableEntity
    {
        public EfDeletableRepository(DbContext ctx)
            : base(ctx)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted() => base.All();

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void RecoverDeleted(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;

            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            this.Update(entity);
        }
    }
}
