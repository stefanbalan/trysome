﻿// <auto-generated />
using System;
using CoL.DB.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoL.DB.Sqlite.Migrations
{
    [DbContext(typeof(CoLContextSqlite))]
    partial class CoLContextSqliteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsWarLogPublic")
                        .HasColumnType("bit");

                    b.Property<int>("MemberCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequiredTownhallLevel")
                        .HasColumnType("int");

                    b.Property<int>("RequiredTrophies")
                        .HasColumnType("int");

                    b.Property<int>("RequiredVersusTrophies")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WarFrequency")
                        .HasColumnType("varchar(50)");

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

            modelBuilder.Entity("CoL.DB.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("CoL.DB.Entities.Member", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("varchar(12)");

                    b.Property<int>("ClanRank")
                        .HasColumnType("int");

                    b.Property<string>("ClanTag")
                        .HasColumnType("varchar(12)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

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

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("nvarchar(1073741823)");

                    b.Property<bool>("IsMember")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLeft")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PreviousClanRank")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Trophies")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("VersusTrophies")
                        .HasColumnType("int");

                    b.HasKey("Tag");

                    b.HasIndex("ClanTag");

                    b.HasIndex("LeagueId");

                    b.ToTable("ClanMembers");
                });

            modelBuilder.Entity("CoL.DB.Entities.War", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttacksPerMember")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PreparationStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Result")
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("State")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("TeamSize")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EndTime");

                    b.ToTable("Wars");
                });

            modelBuilder.Entity("CoL.DB.Entities.WarMember", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("varchar(12)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MapPosition")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OpponentAttacks")
                        .HasColumnType("int");

                    b.Property<int>("TownHallLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WarIdC")
                        .HasColumnType("int");

                    b.Property<int?>("WarIdO")
                        .HasColumnType("int");

                    b.HasKey("Tag");

                    b.HasIndex("WarIdC");

                    b.HasIndex("WarIdO");

                    b.ToTable("WarMember");
                });

            modelBuilder.Entity("CoL.DB.Entities.Clan", b =>
                {
                    b.OwnsOne("CoL.DB.Entities.BadgeUrls", "BadgeUrls", b1 =>
                        {
                            b1.Property<string>("ClanTag")
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("Large")
                                .IsRequired()
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Medium")
                                .IsRequired()
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Small")
                                .IsRequired()
                                .HasColumnType("varchar(150)");

                            b1.HasKey("ClanTag");

                            b1.ToTable("Clans");

                            b1.WithOwner()
                                .HasForeignKey("ClanTag");
                        });

                    b.Navigation("BadgeUrls");
                });

            modelBuilder.Entity("CoL.DB.Entities.League", b =>
                {
                    b.OwnsOne("CoL.DB.Entities.IconUrls", "IconUrls", b1 =>
                        {
                            b1.Property<int>("LeagueId")
                                .HasColumnType("int");

                            b1.Property<string>("Medium")
                                .IsRequired()
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Small")
                                .IsRequired()
                                .HasColumnType("varchar(150)");

                            b1.Property<string>("Tiny")
                                .IsRequired()
                                .HasColumnType("varchar(150)");

                            b1.HasKey("LeagueId");

                            b1.ToTable("Leagues");

                            b1.WithOwner()
                                .HasForeignKey("LeagueId");
                        });

                    b.Navigation("IconUrls");
                });

            modelBuilder.Entity("CoL.DB.Entities.Member", b =>
                {
                    b.HasOne("CoL.DB.Entities.Clan", null)
                        .WithMany("Members")
                        .HasForeignKey("ClanTag");

                    b.HasOne("CoL.DB.Entities.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId");

                    b.Navigation("League");
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
                                .IsRequired()
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.Property<string>("Tag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.HasKey("WarId");

                            b1.HasIndex("Tag");

                            b1.ToTable("Wars");

                            b1.WithOwner()
                                .HasForeignKey("WarId");

                            b1.OwnsOne("CoL.DB.Entities.BadgeUrls", "BadgeUrls", b2 =>
                                {
                                    b2.Property<int>("WarClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Large")
                                        .IsRequired()
                                        .HasColumnType("varchar(150)");

                                    b2.Property<string>("Medium")
                                        .IsRequired()
                                        .HasColumnType("varchar(150)");

                                    b2.Property<string>("Small")
                                        .IsRequired()
                                        .HasColumnType("varchar(150)");

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
                                .IsRequired()
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.Property<string>("Tag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.HasKey("WarId");

                            b1.HasIndex("Tag");

                            b1.ToTable("Wars");

                            b1.WithOwner()
                                .HasForeignKey("WarId");

                            b1.OwnsOne("CoL.DB.Entities.BadgeUrls", "BadgeUrls", b2 =>
                                {
                                    b2.Property<int>("WarClanWarId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Large")
                                        .IsRequired()
                                        .HasColumnType("varchar(150)");

                                    b2.Property<string>("Medium")
                                        .IsRequired()
                                        .HasColumnType("varchar(150)");

                                    b2.Property<string>("Small")
                                        .IsRequired()
                                        .HasColumnType("varchar(150)");

                                    b2.HasKey("WarClanWarId");

                                    b2.ToTable("Wars");

                                    b2.WithOwner()
                                        .HasForeignKey("WarClanWarId");
                                });

                            b1.Navigation("BadgeUrls");
                        });

                    b.Navigation("Clan")
                        .IsRequired();

                    b.Navigation("Opponent")
                        .IsRequired();
                });

            modelBuilder.Entity("CoL.DB.Entities.WarMember", b =>
                {
                    b.HasOne("CoL.DB.Entities.War", null)
                        .WithMany("ClanMembers")
                        .HasForeignKey("WarIdC")
                        .HasConstraintName("FK_WarMember_Wars_WarId_ClanMembers");

                    b.HasOne("CoL.DB.Entities.War", null)
                        .WithMany("OpponentMembers")
                        .HasForeignKey("WarIdO")
                        .HasConstraintName("FK_WarMember_Wars_WarId_OpponentMembers");

                    b.OwnsOne("CoL.DB.Entities.WarAttack", "Attack1", b1 =>
                        {
                            b1.Property<string>("WarMemberTag")
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("AttackerTag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("DefenderTag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.Property<int>("DestructionPercentage")
                                .HasColumnType("int");

                            b1.Property<int>("Duration")
                                .HasColumnType("int");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.HasKey("WarMemberTag");

                            b1.ToTable("WarMember");

                            b1.WithOwner()
                                .HasForeignKey("WarMemberTag");
                        });

                    b.OwnsOne("CoL.DB.Entities.WarAttack", "Attack2", b1 =>
                        {
                            b1.Property<string>("WarMemberTag")
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("AttackerTag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("DefenderTag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.Property<int>("DestructionPercentage")
                                .HasColumnType("int");

                            b1.Property<int>("Duration")
                                .HasColumnType("int");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.HasKey("WarMemberTag");

                            b1.ToTable("WarMember");

                            b1.WithOwner()
                                .HasForeignKey("WarMemberTag");
                        });

                    b.OwnsOne("CoL.DB.Entities.WarAttack", "BestOpponentAttack", b1 =>
                        {
                            b1.Property<string>("WarMemberTag")
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("AttackerTag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.Property<string>("DefenderTag")
                                .IsRequired()
                                .HasColumnType("varchar(12)");

                            b1.Property<int>("DestructionPercentage")
                                .HasColumnType("int");

                            b1.Property<int>("Duration")
                                .HasColumnType("int");

                            b1.Property<int>("Order")
                                .HasColumnType("int");

                            b1.Property<int>("Stars")
                                .HasColumnType("int");

                            b1.HasKey("WarMemberTag");

                            b1.ToTable("WarMember");

                            b1.WithOwner()
                                .HasForeignKey("WarMemberTag");
                        });

                    b.Navigation("Attack1");

                    b.Navigation("Attack2");

                    b.Navigation("BestOpponentAttack");
                });

            modelBuilder.Entity("CoL.DB.Entities.Clan", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("CoL.DB.Entities.War", b =>
                {
                    b.Navigation("ClanMembers");

                    b.Navigation("OpponentMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
