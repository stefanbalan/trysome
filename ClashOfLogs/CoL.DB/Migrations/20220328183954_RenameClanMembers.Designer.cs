﻿// <auto-generated />
using System;
using CoL.DB.mssql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoL.DB.Migrations
{
    [DbContext(typeof(CoLContext))]
    [Migration("20220328183954_RenameClanMembers")]
    partial class RenameClanMembers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoL.DB.Entities.Clan", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("varchar(12)");

                    b.Property<int>("ClanLevel")
                        .HasColumnType("int");

                    b.Property<int>("ClanPoints")
                        .HasColumnType("int");

                    b.Property<int>("ClanVersusPoints")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWarLogPublic")
                        .HasColumnType("bit");

                    b.Property<int>("MembersCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequiredTownhallLevel")
                        .HasColumnType("int");

                    b.Property<int>("RequiredTrophies")
                        .HasColumnType("int");

                    b.Property<int>("RequiredVersusTrophies")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WarFrequency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarLosses")
                        .HasColumnType("int");

                    b.Property<int>("WarTies")
                        .HasColumnType("int");

                    b.Property<int>("WarWinStreak")
                        .HasColumnType("int");

                    b.Property<int>("WarWins")
                        .HasColumnType("int");

                    b.HasKey("Tag");

                    b.ToTable("Clans");
                });

            modelBuilder.Entity("CoL.DB.Entities.Member", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("varchar(12)");

                    b.Property<int>("ClanRank")
                        .HasColumnType("int");

                    b.Property<string>("ClanTag")
                        .HasColumnType("varchar(12)");

                    b.Property<int>("Donations")
                        .HasColumnType("int");

                    b.Property<int>("DonationsPreviousSeason")
                        .HasColumnType("int");

                    b.Property<int>("DonationsReceived")
                        .HasColumnType("int");

                    b.Property<int>("DonationsReceivedPreviousSeason")
                        .HasColumnType("int");

                    b.Property<int>("ExpLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PreviousClanRank")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeFirstSeen")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeLastSeen")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeQuit")
                        .HasColumnType("datetime2");

                    b.Property<int>("Trophies")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("VersusTrophies")
                        .HasColumnType("int");

                    b.HasKey("Tag");

                    b.HasIndex("ClanTag");

                    b.ToTable("ClanMembers");
                });

            modelBuilder.Entity("CoL.DB.Entities.War", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AttacksPerMember")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PreparationStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamSize")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Wars");
                });

            modelBuilder.Entity("CoL.DB.Entities.Clan", b =>
                {
                    b.OwnsOne("CoL.DB.Entities.BadgeUrls", "BadgeUrls", b1 =>
                        {
                            b1.Property<string>("ClanTag")
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("Large")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Medium")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Small")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ClanTag");

                            b1.ToTable("Clans");

                            b1.WithOwner()
                                .HasForeignKey("ClanTag");
                        });

                    b.Navigation("BadgeUrls")
                        .IsRequired();
                });

            modelBuilder.Entity("CoL.DB.Entities.Member", b =>
                {
                    b.HasOne("CoL.DB.Entities.Clan", null)
                        .WithMany("Members")
                        .HasForeignKey("ClanTag");
                });

            modelBuilder.Entity("CoL.DB.Entities.War", b =>
                {
                    b.OwnsOne("CoL.DB.Entities.WarClan", "Clan", b1 =>
                        {
                            b1.Property<int>("WarId")
                                .HasColumnType("int");

                            b1.Property<int>("Attacks")
                                .HasColumnType("int");

                            b1.Property<int>("ClanLevel")
                                .HasColumnType("int");

                            b1.Property<double>("DestructionPercentage")
                                .HasColumnType("float");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.Property<string>("Tag")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("WarId");

                            b1.ToTable("Wars");

                            b1.WithOwner()
                                .HasForeignKey("WarId");

                            b1.OwnsOne("CoL.DB.Entities.BadgeUrls", "BadgeUrls", b2 =>
                                {
                                    b2.Property<int>("WarClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Large")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Medium")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Small")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("WarClanWarId");

                                    b2.ToTable("Wars");

                                    b2.WithOwner()
                                        .HasForeignKey("WarClanWarId");
                                });

                            b1.Navigation("BadgeUrls");
                        });

                    b.OwnsOne("CoL.DB.Entities.WarClan", "Opponent", b1 =>
                        {
                            b1.Property<int>("WarId")
                                .HasColumnType("int");

                            b1.Property<int>("Attacks")
                                .HasColumnType("int");

                            b1.Property<int>("ClanLevel")
                                .HasColumnType("int");

                            b1.Property<double>("DestructionPercentage")
                                .HasColumnType("float");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.Property<string>("Tag")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("WarId");

                            b1.ToTable("Wars");

                            b1.WithOwner()
                                .HasForeignKey("WarId");

                            b1.OwnsOne("CoL.DB.Entities.BadgeUrls", "BadgeUrls", b2 =>
                                {
                                    b2.Property<int>("WarClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Large")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Medium")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Small")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("WarClanWarId");

                                    b2.ToTable("Wars");

                                    b2.WithOwner()
                                        .HasForeignKey("WarClanWarId");
                                });

                            b1.Navigation("BadgeUrls");
                        });

                    b.OwnsMany("CoL.DB.Entities.WarMemberClan", "ClanMembers", b1 =>
                        {
                            b1.Property<int>("WarId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<int>("MapPosition")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("OpponentAttacks")
                                .HasColumnType("int");

                            b1.Property<string>("Tag")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("TownhallLevel")
                                .HasColumnType("int");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("WarId", "Id");

                            b1.ToTable("WarMembers_Clan", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WarId");

                            b1.OwnsOne("CoL.DB.Entities.WarAttack", "Attack1", b2 =>
                                {
                                    b2.Property<int>("WarMemberClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<int>("WarMemberClanId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AttackerTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("DefenderTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("DestructionPercentage")
                                        .HasColumnType("int");

                                    b2.Property<int>("Duration")
                                        .HasColumnType("int");

                                    b2.Property<int>("Order")
                                        .HasColumnType("int");

                                    b2.Property<int>("Stars")
                                        .HasColumnType("int");

                                    b2.HasKey("WarMemberClanWarId", "WarMemberClanId");

                                    b2.ToTable("WarMembers_Clan");

                                    b2.WithOwner()
                                        .HasForeignKey("WarMemberClanWarId", "WarMemberClanId");
                                });

                            b1.OwnsOne("CoL.DB.Entities.WarAttack", "Attack2", b2 =>
                                {
                                    b2.Property<int>("WarMemberClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<int>("WarMemberClanId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AttackerTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("DefenderTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("DestructionPercentage")
                                        .HasColumnType("int");

                                    b2.Property<int>("Duration")
                                        .HasColumnType("int");

                                    b2.Property<int>("Order")
                                        .HasColumnType("int");

                                    b2.Property<int>("Stars")
                                        .HasColumnType("int");

                                    b2.HasKey("WarMemberClanWarId", "WarMemberClanId");

                                    b2.ToTable("WarMembers_Clan");

                                    b2.WithOwner()
                                        .HasForeignKey("WarMemberClanWarId", "WarMemberClanId");
                                });

                            b1.OwnsOne("CoL.DB.Entities.WarAttack", "BestOpponentAttack", b2 =>
                                {
                                    b2.Property<int>("WarMemberClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<int>("WarMemberClanId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AttackerTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("DefenderTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("DestructionPercentage")
                                        .HasColumnType("int");

                                    b2.Property<int>("Duration")
                                        .HasColumnType("int");

                                    b2.Property<int>("Order")
                                        .HasColumnType("int");

                                    b2.Property<int>("Stars")
                                        .HasColumnType("int");

                                    b2.HasKey("WarMemberClanWarId", "WarMemberClanId");

                                    b2.ToTable("WarMembers_Clan");

                                    b2.WithOwner()
                                        .HasForeignKey("WarMemberClanWarId", "WarMemberClanId");
                                });

                            b1.Navigation("Attack1");

                            b1.Navigation("Attack2");

                            b1.Navigation("BestOpponentAttack");
                        });

                    b.OwnsMany("CoL.DB.Entities.WarMemberOpponent", "OpponentMembers", b1 =>
                        {
                            b1.Property<int>("WarId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<int>("MapPosition")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("OpponentAttacks")
                                .HasColumnType("int");

                            b1.Property<string>("Tag")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("TownhallLevel")
                                .HasColumnType("int");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("WarId", "Id");

                            b1.ToTable("WarMembers_Opponent", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("WarId");

                            b1.OwnsOne("CoL.DB.Entities.WarAttack", "Attack1", b2 =>
                                {
                                    b2.Property<int>("WarMemberOpponentWarId")
                                        .HasColumnType("int");

                                    b2.Property<int>("WarMemberOpponentId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AttackerTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("DefenderTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("DestructionPercentage")
                                        .HasColumnType("int");

                                    b2.Property<int>("Duration")
                                        .HasColumnType("int");

                                    b2.Property<int>("Order")
                                        .HasColumnType("int");

                                    b2.Property<int>("Stars")
                                        .HasColumnType("int");

                                    b2.HasKey("WarMemberOpponentWarId", "WarMemberOpponentId");

                                    b2.ToTable("WarMembers_Opponent");

                                    b2.WithOwner()
                                        .HasForeignKey("WarMemberOpponentWarId", "WarMemberOpponentId");
                                });

                            b1.OwnsOne("CoL.DB.Entities.WarAttack", "Attack2", b2 =>
                                {
                                    b2.Property<int>("WarMemberOpponentWarId")
                                        .HasColumnType("int");

                                    b2.Property<int>("WarMemberOpponentId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AttackerTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("DefenderTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("DestructionPercentage")
                                        .HasColumnType("int");

                                    b2.Property<int>("Duration")
                                        .HasColumnType("int");

                                    b2.Property<int>("Order")
                                        .HasColumnType("int");

                                    b2.Property<int>("Stars")
                                        .HasColumnType("int");

                                    b2.HasKey("WarMemberOpponentWarId", "WarMemberOpponentId");

                                    b2.ToTable("WarMembers_Opponent");

                                    b2.WithOwner()
                                        .HasForeignKey("WarMemberOpponentWarId", "WarMemberOpponentId");
                                });

                            b1.OwnsOne("CoL.DB.Entities.WarAttack", "BestOpponentAttack", b2 =>
                                {
                                    b2.Property<int>("WarMemberOpponentWarId")
                                        .HasColumnType("int");

                                    b2.Property<int>("WarMemberOpponentId")
                                        .HasColumnType("int");

                                    b2.Property<string>("AttackerTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("DefenderTag")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<int>("DestructionPercentage")
                                        .HasColumnType("int");

                                    b2.Property<int>("Duration")
                                        .HasColumnType("int");

                                    b2.Property<int>("Order")
                                        .HasColumnType("int");

                                    b2.Property<int>("Stars")
                                        .HasColumnType("int");

                                    b2.HasKey("WarMemberOpponentWarId", "WarMemberOpponentId");

                                    b2.ToTable("WarMembers_Opponent");

                                    b2.WithOwner()
                                        .HasForeignKey("WarMemberOpponentWarId", "WarMemberOpponentId");
                                });

                            b1.Navigation("Attack1");

                            b1.Navigation("Attack2");

                            b1.Navigation("BestOpponentAttack");
                        });

                    b.Navigation("Clan");

                    b.Navigation("ClanMembers");

                    b.Navigation("Opponent");

                    b.Navigation("OpponentMembers");
                });

            modelBuilder.Entity("CoL.DB.Entities.Clan", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
