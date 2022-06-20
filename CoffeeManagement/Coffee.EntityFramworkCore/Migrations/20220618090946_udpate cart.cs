using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee.EntityFramworkCore.Migrations
{
    public partial class udpatecart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SaleCodeId",
                table: "Orders",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleCodeId",
                table: "Orders");
        }
    }
}
