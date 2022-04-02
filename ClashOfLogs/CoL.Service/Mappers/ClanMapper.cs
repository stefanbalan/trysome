using CoL.DB.mssql;

using System;

namespace CoL.Service.Mappers
{
    interface IMapper<TEntity, TModel>
    {
        TModel NewModel(TEntity entity);
        void ToModel(TEntity entity, out TModel model);

    }

    internal class ClanMapper : IMapper<DB.Entities.Clan, ClashOfLogs.Shared.Clan>
    {
        public void ToModel(DB.Entities.Clan entity, out ClashOfLogs.Shared.Clan model)
        {
            throw new NotImplementedException();
        }

        public ClashOfLogs.Shared.Clan NewModel(DB.Entities.Clan entity)
        {
            throw new NotImplementedException();
        }
    }
}
