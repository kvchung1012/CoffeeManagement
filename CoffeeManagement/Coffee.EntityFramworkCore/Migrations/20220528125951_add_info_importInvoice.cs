using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee.EntityFramworkCore.Migrations
{
    public partial class add_info_importInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ImportInvoices",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ImportInvoices",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ImportInvoices",
                type: "nvarchar(1024)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ImportInvoices");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ImportInvoices");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ImportInvoices");
        }
    }
}
