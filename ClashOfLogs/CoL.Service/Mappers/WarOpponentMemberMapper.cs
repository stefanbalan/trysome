using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

internal class WarOpponentMemberMapper : WarMemberMapper, IMapper<DBWarOpponentMember, WarMember>
{
    public override DBWarOpponentMember CreateEntity(WarMember entity, DateTime timeStamp)
        => (DBWarOpponentMember)base.CreateEntity(entity, timeStamp);

    public void UpdateEntity(DBWarOpponentMember entity, WarMember model, DateTime timeStamp)
        => base.UpdateEntity(entity, model, timeStamp);
}
