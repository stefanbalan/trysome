using CoL.DB.Entities;
using CoL.DB.mssql;

using Microsoft.EntityFrameworkCore;

namespace CoL.Service
{
    public abstract class EntityImporter<TDBEntity, TEntity, TKey>
        where TDBEntity : BaseEntity
        where TEntity : class
    {
        protected readonly CoLContext context;

        protected EntityImporter(CoLContext context)
        {
            this.context = context;
        }

        public async Task ImportAsync(TEntity entity, DateTime dateTime)
        {
            var dbEntity = await FindExistingAsync(entity);
            if (dbEntity != null) dbEntity = await CreateNewAsync(entity, dateTime);
            UpdateProperties(dbEntity, entity, dateTime);
            UpdateChildren(dbEntity, entity, dateTime);
            await context.SaveChangesAsync();
        }

        public abstract Task<TDBEntity> FindExistingAsync(TEntity entity);
        public abstract DbSet<TDBEntity> GetDbSet();
        public abstract Func<TEntity, TKey> GetKey { get; }
        public abstract Task<TDBEntity> CreateNewAsync(TEntity entity, DateTime dateTime);
        public abstract void UpdateProperties(TDBEntity tDBEntity, TEntity entity, DateTime dateTime);
        public abstract void UpdateChildren(TDBEntity tDBEntity, TEntity entity, DateTime dateTime);

        protected bool TagsAreEqual(string tag1, string tag2) => string.Equals(tag1, tag2, StringComparison.OrdinalIgnoreCase);
    }
}
