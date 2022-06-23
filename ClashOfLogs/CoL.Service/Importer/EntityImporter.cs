using CoL.DB.Entities;
using CoL.DB.mssql;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    public abstract class EntityImporter<TDBEntity, TEntity, TKey>
        where TDBEntity : BaseEntity
        where TEntity : class
    {
        protected readonly CoLContext context;
        private readonly ILogger<EntityImporter<TDBEntity, TEntity, TKey>> logger;

        protected EntityImporter(CoLContext context,
            ILogger<EntityImporter<TDBEntity, TEntity, TKey>> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> ImportAsync(TEntity entity, DateTime dateTime)
        {
            try
            {
                var dbEntity = await FindExistingAsync(entity);
                if (dbEntity != null) dbEntity = await CreateNewAsync(entity, dateTime);
                UpdateProperties(dbEntity, entity, dateTime);
                UpdateChildren(dbEntity, entity, dateTime);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
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
