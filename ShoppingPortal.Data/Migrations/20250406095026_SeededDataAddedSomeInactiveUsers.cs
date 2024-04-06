using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededDataAddedSomeInactiveUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Firstname", "IsActive", "Lastname", "PasswordHash", "PhoneNumber", "PostalCode", "StreetAddress", "UpdatedAt", "UserType" },
                values: new object[,]
                {
                    { new Guid("a2c3d4e5-2345-6789-0123-bcdef1234567"), new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "tanmaysinh.gharia@gmail.com", "Tanmaysinh", false, "Gharia", "uZJw2fIyuqgeaiT5vTKJP2LrFFHHDTVNJqpSpKFOsoA=", "1234567890", "380051", "JV Park", null, "Customer" },
                    { new Guid("d2c3d4e5-2345-6789-0123-bcdef1234567"), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "boom.baam@gmail.com", "Boom", false, "Baam", "7MJuibqDv3bzFaxzr/LVmHeOxntqzGBdgaAoWgcK6Mc=", "1234562890", "380051", "JV Park", null, "Customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a2c3d4e5-2345-6789-0123-bcdef1234567"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d2c3d4e5-2345-6789-0123-bcdef1234567"));
        }
    }
}
