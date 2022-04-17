using CoL.DB.Entities;

using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Importer
{
    internal interface IRepository<TDbEntity, TKey> where TDbEntity : BaseEntity
    {
        Task<TDbEntity> GetByIdAsync(TKey id);
        void Save(TDbEntity dbLeague);
    }



    internal abstract class BaseRepository<TContext, TDbEntity, TKey> : IRepository<TDbEntity, TKey>
        where TDbEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext context;

        public BaseRepository(TContext context)
        {
            this.context = context;
        }

        public abstract DbSet<TDbEntity> DbSet { get; }
        
        public virtual async Task<TDbEntity> GetByIdAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public abstract void Save(TDbEntity dbLeague);
    }
}