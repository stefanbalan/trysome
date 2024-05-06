namespace CoL.Service.Mappers;

public interface IMapper<TEntity, TModel> where TEntity : DB.Entities.BaseEntity
{
    TEntity CreateAndUpdateEntity(TModel model, DateTime timeStamp);
    bool UpdateEntity(TEntity entity, TModel model, DateTime timeStamp);
}