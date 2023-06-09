using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class WarMemberImporter : EntityImporter<DBWarMember, WarMember>
{
    public WarMemberImporter(
        IMapper<DBWarMember, WarMember> mapper,
        IRepository<DBWarMember> repository,
        ILogger<WarMemberImporter> logger)
        : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(WarMember entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBWarMember dbEntity, WarMember entity, DateTime timestamp)
        => await Task.CompletedTask;
}