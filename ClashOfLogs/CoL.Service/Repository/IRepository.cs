using CoL.DB.Entities;

namespace CoL.Service.Repository;

internal interface IRepository<TDbEntity, TKey> where TDbEntity : BaseEntity
{
    Task Add(TDbEntity entity);
    Task<TDbEntity?> GetByIdAsync(TKey id);
}