using CoL.DB.Entities;

namespace CoL.Service.Repository;

public interface IRepository<TDbEntity> where TDbEntity : BaseEntity
{
    ValueTask<TDbEntity?> GetByIdAsync(params object?[] keyValues);

    void Add(TDbEntity entity);

    void Update(TDbEntity entity);

    ValueTask PersistChangesAsync();
}