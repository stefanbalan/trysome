using CoL.DB.Entities;
using CoL.DB.mssql;

using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository
{
    internal interface IRepository<TDbEntity, TKey> where TDbEntity : BaseEntity
    {
        Task<TDbEntity> GetByIdAsync(TKey id);
    }



    public abstract class BaseRepository<TContext, TDbEntity, TKey> : IRepository<TDbEntity, TKey>
        where TDbEntity : BaseEntity
        where TContext : DbContext
    {
        protected readonly TContext context;

        public BaseRepository(TContext context)
        {
            this.context = context;
        }

        public abstract DbSet<TDbEntity> DbSet { get; }

        public virtual async Task<TDbEntity> GetByIdAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }
    }


    public class MemberRepository : BaseRepository<CoLContext, DBMember, string>
    {
        public MemberRepository(CoLContext context) : base(context)
        {
        }

        public override DbSet<DBMember> DbSet => context.ClanMembers;
    }

    public class LeagueRepository : BaseRepository<CoLContext, DBLeague, int>
    {
        public LeagueRepository(CoLContext context) : base(context)
        {
        }

        public override DbSet<DBLeague> DbSet => context.Leagues;
    }
}