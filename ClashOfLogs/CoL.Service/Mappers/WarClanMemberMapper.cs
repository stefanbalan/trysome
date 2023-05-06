using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

internal class WarClanMemberMapper : WarMemberMapper, IMapper<DBWarClanMember, WarMember>
{
    public override DBWarClanMember CreateEntity(WarMember entity, DateTime timeStamp)
        => (DBWarClanMember)base.CreateEntity(entity, timeStamp);

    public void UpdateEntity(DBWarClanMember entity, WarMember model, DateTime timeStamp)
        => base.UpdateEntity(entity, model, timeStamp);
}
