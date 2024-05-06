// using ClashOfLogs.Shared;
// using CoL.Service.Mappers;
// using CoL.Service.Repository;
// using Microsoft.Extensions.Logging;
//
// namespace CoL.Service.Importer;
//
// internal class WarMemberOpponentImporter : EntityImporter<DBWarOpponentMember, WarMember>
// {
//     public WarMemberOpponentImporter(
//         IMapper<DBWarOpponentMember, WarMember> mapper,
//         IRepository<DBWarOpponentMember> repository,
//         ILogger<WarMemberOpponentImporter> logger) :
//         base(mapper, repository, logger)
//     {
//     }
//
//     protected override object?[] EntityKey(WarMember entity) => new object?[] { entity.Tag };
//
//     protected async override Task UpdateChildrenAsync(DBWarOpponentMember dbEntity, WarMember entity,
//         DateTime timestamp) => await Task.CompletedTask;
// }