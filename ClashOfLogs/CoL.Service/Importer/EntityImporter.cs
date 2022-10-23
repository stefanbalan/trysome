using CoL.DB.Entities;
using CoL.DB.mssql;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    public abstract class EntityImporter<TDbEntity, TEntity, TKey>
        where TDbEntity : BaseEntity
        where TEntity : class
    {
        protected readonly CoLContext context;
        private readonly ILogger<EntityImporter<TDbEntity, TEntity, TKey>> logger;

        protected EntityImporter(CoLContext context,
            ILogger<EntityImporter<TDbEntity, TEntity, TKey>> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> ImportAsync(TEntity entity, DateTime dateTime)
        {
            try
            {
                var dbEntity = await FindExistingAsync(entity) ?? await CreateNewAsync(entity, dateTime);
                UpdateProperties(dbEntity, entity, dateTime);
                await UpdateChildrenAsync(dbEntity, entity, dateTime);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        protected abstract Task<TDbEntity> FindExistingAsync(TEntity entity);
        protected abstract DbSet<TDbEntity> GetDbSet();
        protected abstract Task<TDbEntity> CreateNewAsync(TEntity entity, DateTime dateTime);
        protected abstract Func<TEntity, TKey> GetKey { get; }
        protected abstract void UpdateProperties(TDbEntity tDBEntity, TEntity entity, DateTime dateTime);
        protected abstract Task UpdateChildrenAsync(TDbEntity tDBEntity, TEntity entity, DateTime dateTime);

        protected bool TagsAreEqual(string tag1, string tag2) => string.Equals(tag1, tag2, StringComparison.OrdinalIgnoreCase);
    }
}
