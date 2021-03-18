using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MKTFY.App.Migrations
{
    public partial class AddingSeachEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Faq",
                table: "Faq");

            migrationBuilder.RenameTable(
                name: "Faq",
                newName: "Faqs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faqs",
                table: "Faqs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Searches",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false),
                    SearchTerm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Searches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Searches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faqs",
                table: "Faqs");

            migrationBuilder.RenameTable(
                name: "Faqs",
                newName: "Faq");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faq",
                table: "Faq",
                column: "Id");
        }
    }
}
