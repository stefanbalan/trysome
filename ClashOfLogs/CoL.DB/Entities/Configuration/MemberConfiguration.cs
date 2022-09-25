﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities
{
    public class MemberConfiguration : BaseEntityWithTag.Configuration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);

            builder.HasKey(clan => clan.Tag);

            builder.HasOne<League>();
        }
    }
}