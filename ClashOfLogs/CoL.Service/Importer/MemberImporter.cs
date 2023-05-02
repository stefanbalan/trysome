using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class MemberImporter : EntityImporter<DBMember, Member>
{
    public MemberImporter(
        IMapper<DBMember, Member> mapper,
        IRepository<DBMember> repository,
        ILogger<EntityImporter<DBMember, Member>> logger)
        : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(Member entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBMember dbEntity, Member entity, DateTime timestamp)
        => await Task.CompletedTask;
}