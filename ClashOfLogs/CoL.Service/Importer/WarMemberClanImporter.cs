using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class WarMemberClanImporter : EntityImporter<DBWarClanMember, WarMember>
{
    public WarMemberClanImporter(IMapper<DBWarClanMember, WarMember> mapper, IRepository<DBWarClanMember> repository,
        ILogger<WarMemberClanImporter> logger) : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(WarMember entity) => new object?[] { entity.Tag };

    protected async override Task UpdateChildrenAsync(DBWarClanMember dbEntity, WarMember entity, DateTime timestamp)
        => await Task.CompletedTask;
}
