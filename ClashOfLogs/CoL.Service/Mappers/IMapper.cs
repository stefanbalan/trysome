namespace CoL.Service.Mappers
{
    public interface IMapper<TEntity, TModel> where TEntity : DB.Entities.BaseEntity
    {
        TEntity CreateEntity(TModel entity, DateTime timeStamp);
        ValueTask UpdateEntityAsync(TEntity entity, TModel model, DateTime timeStamp);
    }
}