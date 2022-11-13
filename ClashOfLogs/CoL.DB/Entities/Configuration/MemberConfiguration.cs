using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration
{
    public class MemberConfiguration : BaseEntityWithTag.Configuration<Member>
    {
        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);

            builder.HasOne(cm => cm.League).WithMany();
        }
    }
}
