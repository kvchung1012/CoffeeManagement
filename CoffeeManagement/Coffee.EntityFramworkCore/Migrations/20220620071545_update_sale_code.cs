using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee.EntityFramworkCore.Migrations
{
    public partial class update_sale_code : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxPriceSale",
                table: "SaleCodes",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPriceSale",
                table: "SaleCodes");
        }
    }
}
