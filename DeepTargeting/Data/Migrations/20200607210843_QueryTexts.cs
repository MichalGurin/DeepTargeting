using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeepTargeting.Data.Migrations
{
    public partial class QueryTexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueryTexts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QueryText = table.Column<string>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryTexts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueryTexts");
        }
    }
}
