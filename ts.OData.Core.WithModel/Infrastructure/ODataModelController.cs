using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ts.Domain;

namespace ts.OData.Core.WithModel.Infrastructure
{
    public class ODataModelController<TEntity, TModel> : ODataModelController
        where TModel : ModelConvertibleBase<TEntity, TModel>, new()
    {
        protected IActionResult CreatedModel(TEntity entity) => CreatedModel<TEntity, TModel>(entity);
        protected IActionResult UpdatedModel(TEntity entity) => UpdatedModel<TEntity, TModel>(entity);
    }

    public class ODataModelController : ODataController
    {
        protected static void PatchEntityFromModelDelta<TEntity, TModel>(TEntity entity, Delta<TModel> deltaModel)
            where TModel : ModelConvertibleBase<TEntity, TModel>, new()
        {
            foreach (var changedPropertyName in deltaModel.GetChangedPropertyNames())
            {
                if (!ModelConvertibleBase<TEntity, TModel>.Mappings.ContainsKey(changedPropertyName) ||
                    !deltaModel.TryGetPropertyValue(changedPropertyName, out var newval)) continue;

                var mapping = ModelConvertibleBase<TEntity, TModel>.Mappings[changedPropertyName];
                mapping.GetEntityProperty().SetValue(entity, newval);
            }
        }

        /// <summary>
        /// Creates an action result with the specified values that is a response to a POST operation with an entity
        /// to an entity set.
        /// </summary>
        /// <typeparam name="TEntity">The created entity type.</typeparam>
        /// <typeparam name="TModel">The model type generated from the updated entity. TModel must be <see cref="ModelConvertibleBase{TModel,TEntity}"/> </typeparam>
        /// <param name="entity">The created entity.</param>
        /// <returns>A <see cref="T:System.Web.OData.Results.CreatedODataResult`1" /> with the specified values.</returns>
        protected IActionResult CreatedModel<TEntity, TModel>(TEntity entity)
            where TModel : ModelConvertibleBase<TEntity, TModel>, new()
        {
            return Created(ModelConvertibleBase<TEntity, TModel>.FromEntity(entity));
        }

        /// <summary>
        /// Creates an action result with the specified values that is a response to a PUT, PATCH, or a MERGE operation
        /// on an OData entity.
        /// </summary>
        /// <typeparam name="TEntity">The updated entity type.</typeparam>
        /// <typeparam name="TModel">The model type generated from the updated entity. TModel must be <see cref="ModelConvertibleBase{TModel,TEntity}"/> </typeparam>
        /// <param name="entity">The updated entity.</param>
        /// <returns>An <see cref="T:System.Web.OData.Results.UpdatedODataResult`1" /> the model generated from the specified entity.</returns>
        protected IActionResult UpdatedModel<TEntity, TModel>(TEntity entity)
            where TModel : ModelConvertibleBase<TEntity, TModel>, new()
        {
            return Updated(ModelConvertibleBase<TEntity, TModel>.FromEntity(entity));
        }
    }
}