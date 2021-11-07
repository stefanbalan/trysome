using Microsoft.EntityFrameworkCore.Migrations;

namespace CoL.DB.mssql.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    RequiredTownhallLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.Tag);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clans");
        }
    }
}
