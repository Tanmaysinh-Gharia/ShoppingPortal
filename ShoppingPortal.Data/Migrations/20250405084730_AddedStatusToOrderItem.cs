using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderItems",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("8b9c0d1e-8901-1213-4567-89abcdef1234"),
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: new Guid("9c0d1e2f-9012-1314-5678-9abcdef12345"),
                column: "Status",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderItems");
        }
    }
}
