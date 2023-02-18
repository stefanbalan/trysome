namespace CoL.Service.Mappers
{
    internal interface IMapper<TEntity, TModel> where TEntity : DB.Entities.BaseEntity
    {
        TEntity CreateEntity(TModel entity, DateTime timeStamp);
        Task UpdateEntityAsync(TEntity entity, TModel model, DateTime timeStamp);
    }
}
