using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingDummyEntriesInAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.PostalCode);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Firstname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_PostalCode",
                        column: x => x.PostalCode,
                        principalTable: "Addresses",
                        principalColumn: "PostalCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ShippingPostalCode = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_ShippingPostalCode",
                        column: x => x.ShippingPostalCode,
                        principalTable: "Addresses",
                        principalColumn: "PostalCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCarts_CartId",
                        column: x => x.CartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusLogs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OldStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChangedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_OrderStatusLogs_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStatusLogs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderStatusLogs_Users_ChangedBy",
                        column: x => x.ChangedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "PostalCode", "City", "State" },
                values: new object[,]
                {
                    { "380051", "Ahmedabad", "Gujarat" },
                    { "382481", "Ahmedabad", "Gujarat" },
                    { "393120", "Bharuch", "Gujarat" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Firstname", "IsActive", "Lastname", "PasswordHash", "PhoneNumber", "PostalCode", "StreetAddress", "UpdatedAt", "UserType" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@shoppingportal.com", "Admin", true, "User", "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=", "1234567890", "393120", "123 Admin Street", null, "Admin" },
                    { new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567"), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", true, "Doe", "ZlKyJQ6uc/cQ0FZNsH/AqjoLvXeATZrliDoFdEMm6pk=", "2345678901", "380051", "456 Customer Ave", null, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("d4e5f6a7-4567-8910-1234-56789abcdef1"), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Electronic devices and accessories", "Electronics" },
                    { new Guid("e5f6a7b8-5678-9101-2345-6789abcdef12"), new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Men's and women's clothing", "Clothing" },
                    { new Guid("f6a7b8c9-6789-1011-3456-789abcdef123"), new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Home appliances and kitchenware", "Home & Kitchen" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "ShippingPostalCode", "Status", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "380051", "Delivered", 849.97m, new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567") });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "CartId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("4d5e6f7a-4567-8910-1234-56789abcdef1"), new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567") });

            migrationBuilder.InsertData(
                table: "OrderStatusLogs",
                columns: new[] { "LogId", "ChangedAt", "ChangedBy", "NewStatus", "OldStatus", "OrderId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("0d1e2f3a-0123-1415-6789-abcdef123456"), new DateTime(2023, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Accepted", "Pending", new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"), null },
                    { new Guid("1e2f3a4b-1234-1516-7890-bcdef1234567"), new DateTime(2023, 5, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Delivered", "Accepted", new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"), null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedAt", "CreatedBy", "Description", "ImageUrl", "Name", "Price", "SKU", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"), new Guid("d4e5f6a7-4567-8910-1234-56789abcdef1"), new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Latest smartphone with advanced features", "images/products/smartphone-x.jpg", "Smartphone X", 799.99m, "SPX-1001", 100, null },
                    { new Guid("2b3c4d5e-2345-6789-0123-bcdef1234567"), new Guid("d4e5f6a7-4567-8910-1234-56789abcdef1"), new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Noise cancelling wireless headphones", "images/products/headphones.jpg", "Wireless Headphones", 199.99m, "WH-2002", 50, null },
                    { new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"), new Guid("e5f6a7b8-5678-9101-2345-6789abcdef12"), new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"), "Premium quality cotton t-shirt", "images/products/tshirt.jpg", "Cotton T-Shirt", 24.99m, "CT-3003", 200, null }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "CartItemId", "AddedAt", "CartId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("5e6f7a8b-5678-9101-2345-6789abcdef12"), new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4d5e6f7a-4567-8910-1234-56789abcdef1"), new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"), 1 },
                    { new Guid("6f7a8b9c-6789-1011-3456-789abcdef123"), new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4d5e6f7a-4567-8910-1234-56789abcdef1"), new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"), 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("8b9c0d1e-8901-1213-4567-89abcdef1234"), new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"), new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"), 1, 799.99m },
                    { new Guid("9c0d1e2f-9012-1314-5678-9abcdef12345"), new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"), new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"), 2, 24.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedBy",
                table: "Categories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingPostalCode",
                table: "Orders",
                column: "ShippingPostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusLogs_ChangedBy",
                table: "OrderStatusLogs",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusLogs_OrderId",
                table: "OrderStatusLogs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatusLogs_ProductId",
                table: "OrderStatusLogs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PostalCode",
                table: "Users",
                column: "PostalCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderStatusLogs");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
