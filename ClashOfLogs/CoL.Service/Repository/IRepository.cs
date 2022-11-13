using CoL.DB.Entities;
using CoL.DB.mssql;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository
{
    internal interface IRepository<TDbEntity, TKey> where TDbEntity : BaseEntity
    {
        Task Add(TDbEntity entity);
        Task<TDbEntity> GetByIdAsync(TKey id);
    }


    public abstract class BaseRepository<TContext, TDbEntity, TKey> : IRepository<TDbEntity, TKey>
        where TDbEntity : BaseEntity
        where TContext : DbContext
    {
        protected readonly TContext Context;

        protected BaseRepository(TContext context)
        {
            this.Context = context;
        }

        protected abstract DbSet<TDbEntity> DbSet { get; }

        public async Task Add(TDbEntity entity) => await DbSet.AddAsync(entity);

        public async virtual Task<TDbEntity> GetByIdAsync(TKey id) => await DbSet.FindAsync(id);
    }


    public class MemberRepository : BaseRepository<CoLContext, DBMember, string>
    {
        public MemberRepository(CoLContext context) : base(context)
        {
        }

        protected override DbSet<DBMember> DbSet => Context.ClanMembers;
    }

    public class LeagueRepository : BaseRepository<CoLContext, DBLeague, int>
    {
        public LeagueRepository(CoLContext context) : base(context)
        {
        }

        protected override DbSet<DBLeague> DbSet => Context.Leagues;
    }
}
