using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importers;

public class MemberImporter : EntityImporter<DBMember, Member>
{
    public MemberImporter(
        IMapper<DBMember, Member> mapper,
        IRepository<DBMember> repository,
        ILogger<IEntityImporter<DBMember, Member>> logger)
        : base(mapper, repository, logger)
    {
        // PersistChangesAfterImport = true;
    }

    public override object?[] EntityKey(Member entity) => new object?[] { entity.Tag };

    public async override Task UpdateChildrenAsync(DBMember dbEntity, Member entity, DateTime timestamp)
        => await Task.CompletedTask;
}