using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

public class WarMemberMapperForClan : WarMemberMapper, IMapper<DBWarClanMember, WarMember>
{
    public override DBWarClanMember CreateEntity(WarMember entity, DateTime timeStamp)
        => (DBWarClanMember)base.CreateEntity(entity, timeStamp);

    public bool UpdateEntity(DBWarClanMember entity, WarMember model, DateTime timeStamp)
        => base.UpdateEntity(entity, model, timeStamp);
}
