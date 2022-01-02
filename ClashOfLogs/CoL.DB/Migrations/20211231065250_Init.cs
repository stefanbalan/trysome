using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoL.DB.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "varchar(12)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BadgeUrls_Small = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeUrls_Large = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeUrls_Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClanLevel = table.Column<int>(type: "int", nullable: false),
                    ClanPoints = table.Column<int>(type: "int", nullable: false),
                    ClanVersusPoints = table.Column<int>(type: "int", nullable: false),
                    RequiredTrophies = table.Column<int>(type: "int", nullable: false),
                    WarFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarWinStreak = table.Column<int>(type: "int", nullable: false),
                    WarWins = table.Column<int>(type: "int", nullable: false),
                    WarTies = table.Column<int>(type: "int", nullable: false),
                    WarLosses = table.Column<int>(type: "int", nullable: false),
                    IsWarLogPublic = table.Column<bool>(type: "bit", nullable: false),
                    Members = table.Column<int>(type: "int", nullable: false),
                    RequiredVersusTrophies = table.Column<int>(type: "int", nullable: false),
                    RequiredTownhallLevel = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.Tag);
                });

            migrationBuilder.CreateTable(
                name: "Wars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreparationStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamSize = table.Column<int>(type: "int", nullable: false),
                    AttacksPerMember = table.Column<int>(type: "int", nullable: false),
                    Clan_Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clan_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clan_BadgeUrls_Small = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clan_BadgeUrls_Large = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clan_BadgeUrls_Medium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clan_ClanLevel = table.Column<int>(type: "int", nullable: true),
                    Clan_Attacks = table.Column<int>(type: "int", nullable: true),
                    Clan_Stars = table.Column<int>(type: "int", nullable: true),
                    Clan_DestructionPercentage = table.Column<double>(type: "float", nullable: true),
                    Opponent_Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opponent_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opponent_BadgeUrls_Small = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opponent_BadgeUrls_Large = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opponent_BadgeUrls_Medium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opponent_ClanLevel = table.Column<int>(type: "int", nullable: true),
                    Opponent_Attacks = table.Column<int>(type: "int", nullable: true),
                    Opponent_Stars = table.Column<int>(type: "int", nullable: true),
                    Opponent_DestructionPercentage = table.Column<double>(type: "float", nullable: true)
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
                    ClanTag = table.Column<string>(type: "varchar(12)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpLevel = table.Column<int>(type: "int", nullable: false),
                    Trophies = table.Column<int>(type: "int", nullable: false),
                    VersusTrophies = table.Column<int>(type: "int", nullable: false),
                    ClanRank = table.Column<int>(type: "int", nullable: false),
                    PreviousClanRank = table.Column<int>(type: "int", nullable: false),
                    Donations = table.Column<int>(type: "int", nullable: false),
                    DonationsReceived = table.Column<int>(type: "int", nullable: false),
                    DonationsPreviousSeason = table.Column<int>(type: "int", nullable: false),
                    DonationsReceivedPreviousSeason = table.Column<int>(type: "int", nullable: false),
                    TimeFirstSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeQuit = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "WarMembers_Clan",
                columns: table => new
                {
                    WarId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TownhallLevel = table.Column<int>(type: "int", nullable: false),
                    MapPosition = table.Column<int>(type: "int", nullable: false),
                    Attack1_AttackerTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack1_DefenderTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack1_Stars = table.Column<int>(type: "int", nullable: true),
                    Attack1_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    Attack1_Order = table.Column<int>(type: "int", nullable: true),
                    Attack1_Duration = table.Column<int>(type: "int", nullable: true),
                    Attack2_AttackerTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack2_DefenderTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack2_Stars = table.Column<int>(type: "int", nullable: true),
                    Attack2_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    Attack2_Order = table.Column<int>(type: "int", nullable: true),
                    Attack2_Duration = table.Column<int>(type: "int", nullable: true),
                    OpponentAttacks = table.Column<int>(type: "int", nullable: false),
                    BestOpponentAttack_AttackerTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestOpponentAttack_DefenderTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestOpponentAttack_Stars = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_Order = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_Duration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarMembers_Clan", x => new { x.WarId, x.Id });
                    table.ForeignKey(
                        name: "FK_WarMembers_Clan_Wars_WarId",
                        column: x => x.WarId,
                        principalTable: "Wars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarMembers_Opponent",
                columns: table => new
                {
                    WarId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TownhallLevel = table.Column<int>(type: "int", nullable: false),
                    MapPosition = table.Column<int>(type: "int", nullable: false),
                    Attack1_AttackerTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack1_DefenderTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack1_Stars = table.Column<int>(type: "int", nullable: true),
                    Attack1_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    Attack1_Order = table.Column<int>(type: "int", nullable: true),
                    Attack1_Duration = table.Column<int>(type: "int", nullable: true),
                    Attack2_AttackerTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack2_DefenderTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack2_Stars = table.Column<int>(type: "int", nullable: true),
                    Attack2_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    Attack2_Order = table.Column<int>(type: "int", nullable: true),
                    Attack2_Duration = table.Column<int>(type: "int", nullable: true),
                    OpponentAttacks = table.Column<int>(type: "int", nullable: false),
                    BestOpponentAttack_AttackerTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestOpponentAttack_DefenderTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BestOpponentAttack_Stars = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_DestructionPercentage = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_Order = table.Column<int>(type: "int", nullable: true),
                    BestOpponentAttack_Duration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarMembers_Opponent", x => new { x.WarId, x.Id });
                    table.ForeignKey(
                        name: "FK_WarMembers_Opponent_Wars_WarId",
                        column: x => x.WarId,
                        principalTable: "Wars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanMembers_ClanTag",
                table: "ClanMembers",
                column: "ClanTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanMembers");

            migrationBuilder.DropTable(
                name: "WarMembers_Clan");

            migrationBuilder.DropTable(
                name: "WarMembers_Opponent");

            migrationBuilder.DropTable(
                name: "Clans");

            migrationBuilder.DropTable(
                name: "Wars");
        }
    }
}
