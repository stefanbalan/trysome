using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoL.Service
{
    internal class FileImporter<TContext> where TContext : DbContext
    {
        private readonly TContext context;

        public FileImporter(TContext context)
        {
            this.context = context;
        }

        public async Task Import<TDBEntity, TKey, TEntity>(DbSet<TDBEntity> dbset, TKey key, TEntity entity)
            where TDBEntity : class
            where TEntity : class
        {
            var existing = await dbset.FindAsync(key);

        }
    }
}
