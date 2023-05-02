using CoL.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoL.Service.Repository;

public abstract class BaseEFRepository<TContext, TDbEntity> : IRepository<TDbEntity>
    where TDbEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext Context;

    protected BaseEFRepository(TContext context)
    {
        Context = context;
    }

    protected abstract DbSet<TDbEntity> EntitySet { get; }

    public async virtual ValueTask<TDbEntity?> GetByIdAsync(params object?[] keyValues)
        => await EntitySet.FindAsync(keyValues);

    public async virtual ValueTask AddAsync(TDbEntity entity)
        => await EntitySet.AddAsync(entity);

    public virtual EntityEntry<TDbEntity> Update(TDbEntity entity)
        => EntitySet.Update(entity);

    public async ValueTask PersistChangesAsync()
        => await Context.SaveChangesAsync();
}