using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

public class WarMemberMapperForOpponent : WarMemberMapper, IMapper<DBWarOpponentMember, WarMember>
{
    public override DBWarOpponentMember CreateEntity(WarMember entity, DateTime timeStamp)
        => (DBWarOpponentMember)base.CreateEntity(entity, timeStamp);

    public bool UpdateEntity(DBWarOpponentMember entity, WarMember model, DateTime timeStamp)
        => base.UpdateEntity(entity, model, timeStamp);
}
