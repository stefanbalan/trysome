using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoL.DB.Migrations
{
    public partial class Something : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeFirstSeen",
                table: "ClanMembers");

            migrationBuilder.RenameColumn(
                name: "TimeQuit",
                table: "ClanMembers",
                newName: "LastLeft");

            migrationBuilder.RenameColumn(
                name: "TimeLastSeen",
                table: "ClanMembers",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WarMembers_Opponent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WarMembers_Clan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Clans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMember",
                table: "ClanMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastJoined",
                table: "ClanMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "ClanMembers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrls_Small = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrls_Tiny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrls_Medium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanMembers_LeagueId",
                table: "ClanMembers",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClanMembers_League_LeagueId",
                table: "ClanMembers",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClanMembers_League_LeagueId",
                table: "ClanMembers");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropIndex(
                name: "IX_ClanMembers_LeagueId",
                table: "ClanMembers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WarMembers_Opponent");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WarMembers_Clan");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Clans");

            migrationBuilder.DropColumn(
                name: "IsMember",
                table: "ClanMembers");

            migrationBuilder.DropColumn(
                name: "LastJoined",
                table: "ClanMembers");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "ClanMembers");

            migrationBuilder.RenameColumn(
                name: "LastLeft",
                table: "ClanMembers",
                newName: "TimeQuit");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ClanMembers",
                newName: "TimeLastSeen");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeFirstSeen",
                table: "ClanMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
