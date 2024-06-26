﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoL.DB.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "varchar(12)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Type = table.Column<string>(type: "varchar(10)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    BadgeUrls_Small = table.Column<string>(type: "varchar(150)", nullable: true),
                    BadgeUrls_Medium = table.Column<string>(type: "varchar(150)", nullable: true),
                    BadgeUrls_Large = table.Column<string>(type: "varchar(150)", nullable: true),
                    ClanLevel = table.Column<int>(type: "int", nullable: false),
                    ClanPoints = table.Column<int>(type: "int", nullable: false),
                    ClanVersusPoints = table.Column<int>(type: "int", nullable: false),
                    RequiredTrophies = table.Column<int>(type: "int", nullable: false),
                    WarFrequency = table.Column<string>(type: "varchar(50)", nullable: true),
                    WarWinStreak = table.Column<int>(type: "int", nullable: false),
                    WarWins = table.Column<int>(type: "int", nullable: false),
                    WarTies = table.Column<int>(type: "int", nullable: false),
                    WarLosses = table.Column<int>(type: "int", nullable: false),
                    IsWarLogPublic = table.Column<bool>(type: "bit", nullable: false),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    RequiredVersusTrophies = table.Column<int>(type: "int", nullable: false),
                    RequiredTownhallLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.Tag);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    IconUrls_Small = table.Column<string>(type: "varchar(150)", nullable: true),
                    IconUrls_Tiny = table.Column<string>(type: "varchar(150)", nullable: true),
                    IconUrls_Medium = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(type: "varchar(10)", nullable: true),
                    State = table.Column<string>(type: "varchar(10)", nullable: true),
                    PreparationStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamSize = table.Column<int>(type: "int", nullable: false),
                    AttacksPerMember = table.Column<int>(type: "int", nullable: false),
                    Clan_Tag = table.Column<string>(type: "varchar(12)", nullable: false),
                    Clan_Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Clan_BadgeUrls_Small = table.Column<string>(type: "varchar(150)", nullable: true),
                    Clan_BadgeUrls_Medium = table.Column<string>(type: "varchar(150)", nullable: true),
                    Clan_BadgeUrls_Large = table.Column<string>(type: "varchar(150)", nullable: true),
                    Clan_ClanLevel = table.Column<int>(type: "int", nullable: false),
                    Clan_Attacks = table.Column<int>(type: "int", nullable: false),
                    Clan_Stars = table.Column<int>(type: "int", nullable: false),
                    Clan_DestructionPercentage = table.Column<double>(type: "float", nullable: false),
                    Opponent_Tag = table.Column<string>(type: "varchar(12)", nullable: false),
                    Opponent_Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Opponent_BadgeUrls_Small = table.Column<string>(type: "varchar(150)", nullable: true),
                    Opponent_BadgeUrls_Medium = table.Column<string>(type: "varchar(150)", nullable: true),
                    Opponent_BadgeUrls_Large = table.Column<string>(type: "varchar(150)", nullable: true),
                    Opponent_ClanLevel = table.Column<int>(type: "int", nullable: false),
                    Opponent_Attacks = table.Column<int>(type: "int", nullable: false),
                    Opponent_Stars = table.Column<int>(type: "int", nullable: false),
                    Opponent_DestructionPercentage = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClanMembers",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "varchar(12)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Role = table.Column<string>(type: "varchar(10)", nullable: true),
                    ExpLevel = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: true),
                    Trophies = table.Column<int>(type: "int", nullable: false),
                    VersusTrophies = table.Column<int>(type: "int", nullable: false),
                    ClanRank = table.Column<int>(type: "int", nullable: false),
                    PreviousClanRank = table.Column<int>(type: "int", nullable: false),
                    Donations = table.Column<int>(type: "int", nullable: false),
                    DonationsReceived = table.Column<int>(type: "int", nullable: false),
                    DonationsPreviousSeason = table.Column<int>(type: "int", nullable: false),
                    DonationsReceivedPreviousSeason = table.Column<int>(type: "int", nullable: false),
                    LastLeft = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMember = table.Column<bool>(type: "bit", nullable: false),
                    History = table.Column<string>(type: "nvarchar(1073741823)", nullable: false),
                    ClanTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanMembers", x => x.Tag);
                    table.ForeignKey(
                        name: "FK_ClanMembers_Clans_ClanTag",
                        column: x => x.ClanTag,
                        principalTable: "Clans",
                        principalColumn: "Tag");
                    table.ForeignKey(
                        name: "FK_ClanMembers_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarMember",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "varchar(12)", nullable: false),
                    WarIdC = table.Column<int>(type: "int", nullable: true),
                    WarIdO = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TownHallLevel = table.Column<int>(type: "int", nullable: false),
                    MapPosition = table.Column<int>(type: "int", nullable: false),
                    Attack1_AttackerTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    Attack1_DefenderTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    Attack1_Stars = table.Column<int>(type: "int", nullable: true),
                    Attack1_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    Attack1_Order = table.Column<int>(type: "int", nullable: true),
                    Attack1_Duration = table.Column<int>(type: "int", nullable: true),
                    Attack2_AttackerTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    Attack2_DefenderTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    Attack2_Stars = table.Column<int>(type: "int", nullable: true),
                    Attack2_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    Attack2_Order = table.Column<int>(type: "int", nullable: true),
                    Attack2_Duration = table.Column<int>(type: "int", nullable: true),
                    OpponentAttacks = table.Column<int>(type: "int", nullable: false),
                    BestOpponentAttack_AttackerTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    BestOpponentAttack_DefenderTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    BestOpponentAttack_Stars = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_Order = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_Duration = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarMember", x => x.Tag);
                    table.ForeignKey(
                        name: "FK_WarMember_Wars_WarId_ClanMembers",
                        column: x => x.WarIdC,
                        principalTable: "Wars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarMember_Wars_WarId_OpponentMembers",
                        column: x => x.WarIdO,
                        principalTable: "Wars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanMembers_ClanTag",
                table: "ClanMembers",
                column: "ClanTag");

            migrationBuilder.CreateIndex(
                name: "IX_ClanMembers_LeagueId",
                table: "ClanMembers",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_WarMember_WarIdC",
                table: "WarMember",
                column: "WarIdC");

            migrationBuilder.CreateIndex(
                name: "IX_WarMember_WarIdO",
                table: "WarMember",
                column: "WarIdO");

            migrationBuilder.CreateIndex(
                name: "IX_Wars_Clan_Tag",
                table: "Wars",
                column: "Clan_Tag");

            migrationBuilder.CreateIndex(
                name: "IX_Wars_EndTime",
                table: "Wars",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_Wars_Opponent_Tag",
                table: "Wars",
                column: "Opponent_Tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanMembers");

            migrationBuilder.DropTable(
                name: "WarMember");

            migrationBuilder.DropTable(
                name: "Clans");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Wars");
        }
    }
}
