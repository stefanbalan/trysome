using CoL.Service.EntityModelMapper;

namespace CoL.Service.Mappers;

public abstract class BaseMapper<TEntity, TModel>
    : EntityModelMapper<TEntity, TModel>,
        IMapper<TEntity, TModel>
    where TEntity : DB.Entities.BaseEntity, new()
    where TModel : new()
{
    public virtual TEntity CreateAndUpdateEntity(TModel model, DateTime timeStamp)
    {
        var result = Get1From(model);
        result.CreatedAt = timeStamp;
        result.UpdatedAt = timeStamp;
        return result;
    }


    public virtual bool UpdateEntity(TEntity entity, TModel model, DateTime timeStamp)
    {
        var changed = UpdateT1FromT2(entity, model);
        if (changed) entity.UpdatedAt = timeStamp;
        return changed;
    }
}