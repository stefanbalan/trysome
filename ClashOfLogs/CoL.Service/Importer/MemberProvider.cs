using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;

namespace CoL.Service.Importer
{
    internal class MemberProvider : EntityProviderBase<DBMember, string, Member>
    {
        public MemberProvider(IRepository<DBMember, string> repository, IMapper<DBMember, Member> mapper) : base(repository, mapper)
        {
        }

        protected override string EntityKey(Member model) => model.Tag;
    }
}
