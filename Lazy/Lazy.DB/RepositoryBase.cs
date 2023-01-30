using Microsoft.EntityFrameworkCore;

namespace Lazy.DB
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity, int>
        where TEntity : EntityBaseIntKey
        where TContext : DbContext
    {
        protected readonly TContext context;

        protected RepositoryBase(TContext dbcontext)
        {
            context = dbcontext;
        }

        protected abstract DbSet<TEntity> Set { get; }

        public IQueryable<TEntity> All => Set as IQueryable<TEntity>;

        public virtual TEntity Create(TEntity entity)
        {
            Set.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual TEntity Read(int id)
        {
            return Set.Find(id);
        }

        public virtual TEntity Update(TEntity entity)
        {
            Set.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Set.Remove(entity);
            context.SaveChanges();
        }
    }
}
