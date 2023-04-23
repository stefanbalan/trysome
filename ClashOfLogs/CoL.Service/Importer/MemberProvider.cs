using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class MemberProvider : EntityImporter<DBMember, Member>
{
    public MemberProvider(
        IMapper<DBMember, Member> mapper,
        IRepository<DBMember> repository,
        ILogger<EntityImporter<DBMember, Member>> logger)
        : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(Member entity) => new object?[] { entity.Tag };

    protected override Task UpdateChildrenAsync(DBMember tDbEntity, Member entity, DateTime timestamp)
        => Task.CompletedTask;
}