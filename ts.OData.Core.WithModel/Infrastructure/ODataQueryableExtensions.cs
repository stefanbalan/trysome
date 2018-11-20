using System.Linq;
using ts.Domain;

namespace ts.OData.Core.WithModel.Infrastructure
{
    public static class ODataQueryableExtensions
    {
        public static IQueryable<TModel> ProjectToModel<TEntity, TModel>(this IQueryable<TEntity> query)
            where TModel : ModelConvertibleBase<TEntity , TModel>, new()
        {
            return new QueryableODataProjection<TEntity, TModel>(query);
        }
    }
}