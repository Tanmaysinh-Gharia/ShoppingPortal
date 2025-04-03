using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedimageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"),
                column: "ImageUrl",
                value: "images/products/smartphone-x.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("2b3c4d5e-2345-6789-0123-bcdef1234567"),
                column: "ImageUrl",
                value: "images/products/headphones.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"),
                column: "ImageUrl",
                value: "images/products/tshirt.jpg");
        }
    }
}
