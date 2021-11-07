using CoL.DB.mssql;

using System;

namespace CoL.Service.Mappers
{
    internal class ClanMapper : ModelConvertibleBase<CoL.DB.Entities.Clan, ClashOfLogs.Shared.Clan>
    {
        public override DB.Entities.Clan ToEntity()
        {
            throw new NotImplementedException();
        }

        protected override ClashOfLogs.Shared.Clan FromEntityToThisModel(DB.Entities.Clan entity)
        {
            throw new NotImplementedException();
        }
    }
}
