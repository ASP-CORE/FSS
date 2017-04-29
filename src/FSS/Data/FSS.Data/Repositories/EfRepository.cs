namespace FSS.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FSS.Data.Shared.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public EfRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("DbContext is null");

            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual IQueryable<TEntity> All() => this.DbSet.AsQueryable();

        public void Dispose() => this.Context.Dispose();

        public TEntity GetById(object id) => this.DbSet.Find(id);

        public Task<TEntity> GetByIdAsync(object id) => this.DbSet.FindAsync(id);

        public int SaveChanges() => this.Context.SaveChanges();

        public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if(entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Add(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }
    }
}
