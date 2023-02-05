using System.Linq.Expressions;
using Lazy.Data;
using Microsoft.EntityFrameworkCore;

namespace Lazy.EF.Repository
{
    public abstract class RepositoryEF<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly LazyContext Context;

        protected RepositoryEF(LazyContext context)
        {
            Context = context;
        }

        protected abstract DbSet<TEntity> Set { get; }

        public IQueryable<TEntity> All => Set;

        public virtual TEntity Create(TEntity model)
        {
            Set.Add(model);
            Context.SaveChanges();
            return model;
        }

        public TEntity? Read(TKey id)
        {
            var entity = Set.Find(id);
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            Set.Update(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Set.Remove(entity);
            Context.SaveChanges();
        }

        public virtual PagedRepositoryResult<TEntity> GetPaged(
            int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>>? filterExpression,
            Expression<Func<TEntity, bool>>? sortExpression,
            Expression<Func<TEntity, TEntity>>? projection = null)
        {
            var q = (IQueryable<TEntity>)Set;

            if (filterExpression != null)
                q = q.Where(filterExpression);
            if (sortExpression != null)
                q = q.OrderBy(sortExpression);

            q = q.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (projection != null) q = q.Select(projection);

            var count = q.Count();
            var list = q.ToList();

            var result = new PagedRepositoryResult<TEntity>
            {
                PageSize = pageSize,
                PageNumber = Math.Min(pageNumber, (int)Math.Ceiling(count / (decimal)pageSize)),
                Count = count,
                Results = list
            };
            return result;
        }
    }
}