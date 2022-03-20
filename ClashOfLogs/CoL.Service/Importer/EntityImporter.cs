using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

namespace CoL.Service
{
    internal abstract class EntityImporter<TContext, TDBEntity, TEntity>
        where TContext : DbContext
        where TDBEntity : class
        where TEntity : class
    {
        protected readonly TContext context;

        public EntityImporter(TContext context)
        {
            this.context = context;
        }

        public async Task Import<TKey>(DbSet<TDBEntity> dbset, TKey key, TEntity entity, DateTime dateTime)
        {
            var dbEntity = await dbset.FindAsync(key);
            if (dbEntity != null) dbEntity = CreateNew(entity, dateTime);
            UpdateProperties(dbEntity, entity, dateTime);
            UpdateChildren(dbEntity, entity, dateTime);
            await context.SaveChangesAsync();
        }

        public abstract TDBEntity CreateNew(TEntity entity, DateTime dateTime);
        public abstract void UpdateProperties(TDBEntity tDBEntity, TEntity entity, DateTime dateTime);
        public abstract void UpdateChildren(TDBEntity tDBEntity, TEntity entity, DateTime dateTime);
    }
}
