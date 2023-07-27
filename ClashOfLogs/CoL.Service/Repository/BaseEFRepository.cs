using CoL.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoL.Service.Repository;

public class BaseEFRepository<TContext, TDbEntity> : IRepository<TDbEntity>
    where TDbEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext Context;

    protected BaseEFRepository(TContext context)
    {
        Context = context;
        EntitySet = Context.Set<TDbEntity>();
    }

    protected virtual DbSet<TDbEntity> EntitySet { get; }

    public async virtual ValueTask<TDbEntity?> GetByIdAsync(params object?[] keyValues)
        => await EntitySet.FindAsync(keyValues);

    public virtual void Add(TDbEntity entity)
    {
        var unused = EntitySet.Add(entity);
    }

    public virtual void Update(TDbEntity entity)
    {
        var unused = EntitySet.Update(entity);
    }

    public async ValueTask PersistChangesAsync()
        => await Context.SaveChangesAsync();
}