using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcCoreProductOperationApplication.Migrations
{
    public partial class categoryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinStockQuantity",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinStockQuantity",
                table: "Categories");
        }
    }
}
