using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee.EntityFramworkCore.Migrations
{
    public partial class editcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SystemTableColumns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QueryData",
                table: "SystemTableColumns",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "SystemTableColumns");

            migrationBuilder.DropColumn(
                name: "QueryData",
                table: "SystemTableColumns");
        }
    }
}
