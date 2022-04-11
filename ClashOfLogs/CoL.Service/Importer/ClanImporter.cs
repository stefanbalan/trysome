using ClashOfLogs.Shared;

using CoL.DB.mssql;
using CoL.Service.Mappers;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace CoL.Service.Importer
{
    internal class ClanImporter : EntityImporter<DBClan, Clan, string>
    {
        private readonly IMapper<DBClan, Clan> clanMapper;
        private readonly IMapper<DB.Entities.Member, Member> memberMapper;

        public ClanImporter(
            CoLContext context,
            IMapper<DBClan, Clan> clanMapper,
            IMapper<DBMember, Member> memberMapper
            ) : base(context)
        {
            this.clanMapper = clanMapper;
            this.memberMapper = memberMapper;
        }

        public override Func<Clan, string> GetKey => (clan) => clan.Tag;


        public override async Task<DBClan> FindExistingAsync(Clan entity)
        {
            var dbEntity = await context.Clans
                .Include(clan => clan.Members.Where(member => member.IsMember))
                .FirstOrDefaultAsync(clan => clan.Tag == entity.Tag);

            return dbEntity;
        }

        public override DbSet<DBClan> GetDbSet() => context.Clans;

        public override void UpdateChildren(DBClan dbClan, Clan clan, DateTime timeStamp)
        {
            foreach (var dbMember in dbClan.Members)
            {
                var member = clan.Members.Find(memberEntity => TagsAreEqual(memberEntity.Tag, dbMember.Tag));
                if (member is null)
                {
                    dbMember.LastLeft = timeStamp;
                    dbMember.IsMember = false;
                }
                else
                {
                    memberMapper.UpdateEntity(dbMember, member, timeStamp);
                }
            }
            var nonMembers = clan.Members
                .Where(member => !dbClan.Members.Any(dbMember => TagsAreEqual(dbMember.Tag, member.Tag)));
            var nonMembersTags = nonMembers.Select(member => member.Tag);
            var existingNonMembers = context.ClanMembers
                .Where(member => member.ClanTag == dbClan.Tag && nonMembersTags.Contains(member.Tag));

            foreach (var member in nonMembers)
            {
                var exisingNonMember = existingNonMembers.FirstOrDefault(dbMember => TagsAreEqual(dbMember.Tag, member.Tag));
                if (exisingNonMember is null)
                {
                    var newMember = memberMapper.CreateEntity(member, timeStamp);
                    newMember.ClanTag = dbClan.Tag;
                    dbClan.Members.Add(newMember);
                }
                else
                {
                    memberMapper.UpdateEntity(exisingNonMember, member, timeStamp);
                    exisingNonMember.IsMember = true;
                    exisingNonMember.LastJoined = timeStamp;
                    dbClan.Members.Add(exisingNonMember);
                }
            }
        }


        public override async Task<DBClan> CreateNewAsync(Clan entity, DateTime timeStamp)
        {
            var dbclan = clanMapper.CreateEntity(entity, timeStamp);
            await context.Clans.AddAsync(dbclan);
            return dbclan;
        }

        public override void UpdateProperties(DBClan entity, Clan model, DateTime timeStamp)
        {
            clanMapper.UpdateEntity(entity, model, timeStamp);
        }
    }
}
