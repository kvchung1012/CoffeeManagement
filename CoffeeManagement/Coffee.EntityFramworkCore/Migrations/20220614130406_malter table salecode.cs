using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffee.EntityFramworkCore.Migrations
{
    public partial class maltertablesalecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "SaleCodes",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "SaleCodes");
        }
    }
}
