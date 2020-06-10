using Microsoft.EntityFrameworkCore.Migrations;

namespace DeepTargeting.Data.Migrations
{
    public partial class RenamingQueriesDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QueryTexts",
                table: "QueryTexts");

            migrationBuilder.RenameTable(
                name: "QueryTexts",
                newName: "AllQueries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllQueries",
                table: "AllQueries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AllQueries",
                table: "AllQueries");

            migrationBuilder.RenameTable(
                name: "AllQueries",
                newName: "QueryTexts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueryTexts",
                table: "QueryTexts",
                column: "Id");
        }
    }
}
