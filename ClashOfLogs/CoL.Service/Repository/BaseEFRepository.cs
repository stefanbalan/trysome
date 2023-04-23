using CoL.DB.Entities;
using Microsoft.EntityFrameworkCore;

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

    protected abstract DbSet<TDbEntity> DbSet { get; }

    public async Task Add(TDbEntity entity) => await DbSet.AddAsync(entity);

    public async virtual Task<TDbEntity?> GetByIdAsync(params object?[] keyValues)
        => await DbSet.FindAsync(keyValues);
}