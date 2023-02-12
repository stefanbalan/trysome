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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber">Page numbers start at 0</param>
        /// <param name="filterExpression"></param>
        /// <param name="sortExpression"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        public virtual async Task<PagedRepositoryResult<TEntity>> GetPagedAsync(int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>>? filterExpression,
            Expression<Func<TEntity, bool>>? sortExpression,
            Expression<Func<TEntity, TEntity>>? projection = null)
        {
            var q = (IQueryable<TEntity>)Set;

            if (filterExpression != null)
                q = q.Where(filterExpression);
            var count = await q.CountAsync();

            var page = q;
            if (sortExpression != null)
                page = page.OrderBy(sortExpression);

            page = page.Skip(pageNumber * pageSize).Take(pageSize);

            if (projection != null) page = page.Select(projection);
            
            var list = await page.ToListAsync();

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