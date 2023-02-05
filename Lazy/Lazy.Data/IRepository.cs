using System.Linq.Expressions;

namespace Lazy.Data
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity? Create(TEntity? model);
        TEntity? Read(TKey id);

        TEntity? Update(TEntity model);
        void Delete(TEntity model);

        PagedRepositoryResult<TEntity> GetPaged(
            int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>>? filterExpression,
            Expression<Func<TEntity, bool>>? sortExpression,
            Expression<Func<TEntity, TEntity>>? projection = null);
    }
}