namespace CoL.Service.Mappers;

public abstract class BaseMapper<TEntity, TModel> : IMapper<TEntity, TModel>
    where TEntity : DB.Entities.BaseEntity, new()
{
    public virtual TEntity CreateEntity(TModel entity, DateTime timeStamp) => new()
    {
        CreatedAt = timeStamp,
        UpdatedAt = timeStamp
    };

    public virtual void UpdateEntity(TEntity entity, TModel model, DateTime timeStamp) => entity.UpdatedAt = timeStamp;
}