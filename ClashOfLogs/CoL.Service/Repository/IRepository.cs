using CoL.DB.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoL.Service.Repository;

public interface IRepository<TDbEntity> where TDbEntity : BaseEntity
{
    ValueTask<TDbEntity?> GetByIdAsync(params object?[] keyValues);

    ValueTask AddAsync(TDbEntity entity);

    EntityEntry<TDbEntity> Update(TDbEntity entity);

    ValueTask PersistChangesAsync();
}