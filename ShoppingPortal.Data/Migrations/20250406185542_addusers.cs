using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class addusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Firstname", "IsActive", "Lastname", "PasswordHash", "PhoneNumber", "PostalCode", "StreetAddress", "UpdatedAt", "UserType" },
                values: new object[] { new Guid("b1c3d4e5-2345-6789-0123-bcdef1234567"), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "tanmay.sinh@gmail.com", "Tanmaysinh", true, "Gharia", "uZJw2fIyuqgeaiT5vTKJP2LrFFHHDTVNJqpSpKFOsoA=", "1234567890", "380051", "JV Park", null, "Customer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b1c3d4e5-2345-6789-0123-bcdef1234567"));
        }
    }
}
