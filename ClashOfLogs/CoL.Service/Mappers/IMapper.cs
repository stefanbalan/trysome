namespace CoL.Service.Mappers
{
    interface IMapper<TEntity, TModel> where TEntity : DB.Entities.BaseEntity
    {
        TEntity CreateEntity(TModel entity, DateTime timeStamp);
        void UpdateEntity(TEntity entity, TModel model, DateTime timeStamp);

    }
}
