using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lazy.DB.Migrations
{
    /// <inheritdoc />
    public partial class _1EmailTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Html = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Html", "Name", "Text" },
                values: new object[,]
                {
                    { 1, true, "Template 1", "An <em>example</em> of rich text." },
                    { 2, true, "Template 2", "An <em>example</em> of rich text." },
                    { 3, true, "Template 3", "An <em>example</em> of rich text." },
                    { 4, true, "Template 4", "An <em>example</em> of rich text." },
                    { 5, true, "Template 5", "An <em>example</em> of rich text." },
                    { 6, true, "Template 6", "An <em>example</em> of rich text." },
                    { 7, true, "Template 7", "An <em>example</em> of rich text." },
                    { 8, true, "Template 8", "An <em>example</em> of rich text." },
                    { 9, true, "Template 9", "An <em>example</em> of rich text." },
                    { 10, true, "Template 10", "An <em>example</em> of rich text." },
                    { 11, true, "Template 11", "An <em>example</em> of rich text." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");
        }
    }
}
