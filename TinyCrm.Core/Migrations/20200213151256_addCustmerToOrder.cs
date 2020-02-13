using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCrm.Core.Migrations
{
    public partial class addCustmerToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "core",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "core",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "core",
                table: "Order",
                column: "CustomerId",
                principalSchema: "core",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "core",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                schema: "core",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "core",
                table: "Order",
                column: "CustomerId",
                principalSchema: "core",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
