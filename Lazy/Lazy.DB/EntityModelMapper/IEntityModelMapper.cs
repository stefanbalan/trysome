namespace Lazy.DB.EntityModelMapper
{
    public interface IEntityModelMapper<TEntity, TModel>
    {
        static abstract TModel ToModel(TEntity entity);

        static abstract TEntity ToEntity(TModel model);

        TModel GetModelFrom(TEntity entity);
        TEntity GetEntityFrom(TModel model);
    }
}