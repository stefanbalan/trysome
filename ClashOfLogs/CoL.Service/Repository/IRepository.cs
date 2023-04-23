using System.Linq;
using CoL.DB;
using CoL.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public interface IRepository<TDbEntity> where TDbEntity : BaseEntity
{
    Task<TDbEntity?> GetByIdAsync(params object?[] keyValues);

    Task Add(TDbEntity entity);
}

internal class ClanRepository : IRepository<DBClan>
{
    private readonly CoLContext context;

    public ClanRepository(CoLContext context)
    {
        this.context = context;
    }


    public async Task<Clan?> GetByIdAsync(params object?[] keyValues)
    {
        var dbEntity = await context.Clans
            .Include(clan => clan.Members.Where(member => member.IsMember))
            .FirstOrDefaultAsync(clan => clan.Tag == (string)keyValues[0]);

        return dbEntity;
    }

    public async Task Add(Clan entity) => await context.Clans.AddAsync(entity);
}