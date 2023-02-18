using System.Linq.Expressions;

namespace Lazy.Data
{
    public interface IRepository<TEntity, TKey>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        
        Task<TEntity?> ReadAsync(TKey id);

        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task DeleteAsync(TEntity entity);

        Task<PagedRepositoryResult<TEntity>> ReadPagedAsync(
            int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>>? filterExpression,
            Expression<Func<TEntity, bool>>? sortExpression,
            Expression<Func<TEntity, TEntity>>? projection = null);
    }
}