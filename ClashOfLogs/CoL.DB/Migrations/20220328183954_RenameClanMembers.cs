using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoL.DB.Migrations
{
    public partial class RenameClanMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Members",
                table: "Clans",
                newName: "MembersCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MembersCount",
                table: "Clans",
                newName: "Members");
        }
    }
}
