using System.Linq.Expressions;
using Lazy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lazy.EF.Repository
{
    public abstract class RepositoryEF<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly LazyContext Context;
        protected readonly ILogger<RepositoryEF<TEntity, TKey>> Logger;

        protected RepositoryEF(ILogger<RepositoryEF<TEntity, TKey>> logger, LazyContext context)
        {
            Context = context;
            Logger = logger;
        }

        protected abstract DbSet<TEntity> Set { get; }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                Set.Add(entity);
                await Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                Logger.LogError("Error creating {tpye}: {message}", typeof(TEntity).Name, e.Message);
                throw new RepositoryException(typeof(TEntity), "Error creating", e);
            }
        }

        public async Task<TEntity?> ReadAsync(TKey id)
        {
            try
            {
                var entity = await Set.FindAsync(id);
                return entity;
            }
            catch (Exception e)
            {
                Logger.LogError("Error reading {tpye}: {message}", typeof(TEntity).Name, e.Message);
                throw new RepositoryException(typeof(TEntity), "Error reading", e);
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                Set.Update(entity);
                await Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                Logger.LogError("Error updating {tpye}: {message}", typeof(TEntity).Name, e.Message);
                throw new RepositoryException(typeof(TEntity), "Error updating", e);
            }
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            try
            {
                Set.Remove(entity);
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger.LogError("Error deleting {tpye}: {message}", typeof(TEntity).Name, e.Message);
                throw new RepositoryException(typeof(TEntity), "Error deleting", e);
            }
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
        public virtual async Task<PagedRepositoryResult<TEntity>> ReadPagedAsync(int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>>? filterExpression,
            Expression<Func<TEntity, bool>>? sortExpression,
            Expression<Func<TEntity, TEntity>>? projection = null)
        {
            try
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
            catch (Exception e)
            {
                Logger.LogError("Error reading paged {tpye}: {message}", typeof(TEntity).Name, e.Message);
                throw new RepositoryException(typeof(TEntity), "Error reading paged", e);
            }
        }
    }
}