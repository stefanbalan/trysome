// ReSharper disable UnusedMember.Global

namespace Lazy.Util.EntityModelMapper;


public interface IEntityModelMapper<TEntity, TModel>
{
    static abstract TModel ToModel(TEntity entity);

    static abstract TEntity ToEntity(TModel model);

    TModel GetModelFrom(TEntity entity);
    TEntity GetEntityFrom(TModel model);
    bool UpdateEntityFromModel(TEntity entity, TModel model);
}
