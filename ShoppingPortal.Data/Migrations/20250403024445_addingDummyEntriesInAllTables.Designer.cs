﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingPortal.Data.Context;

#nullable disable

namespace ShoppingPortal.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250403024445_addingDummyEntriesInAllTables")]
    partial class addingDummyEntriesInAllTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Address", b =>
                {
                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PostalCode");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            PostalCode = "393120",
                            City = "Bharuch",
                            State = "Gujarat"
                        },
                        new
                        {
                            PostalCode = "380051",
                            City = "Ahmedabad",
                            State = "Gujarat"
                        },
                        new
                        {
                            PostalCode = "382481",
                            City = "Ahmedabad",
                            State = "Gujarat"
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.CartItem", b =>
                {
                    b.Property<Guid>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");

                    b.HasData(
                        new
                        {
                            CartItemId = new Guid("5e6f7a8b-5678-9101-2345-6789abcdef12"),
                            AddedAt = new DateTime(2023, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CartId = new Guid("4d5e6f7a-4567-8910-1234-56789abcdef1"),
                            ProductId = new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"),
                            Quantity = 1
                        },
                        new
                        {
                            CartItemId = new Guid("6f7a8b9c-6789-1011-3456-789abcdef123"),
                            AddedAt = new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CartId = new Guid("4d5e6f7a-4567-8910-1234-56789abcdef1"),
                            ProductId = new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"),
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("d4e5f6a7-4567-8910-1234-56789abcdef1"),
                            CreatedAt = new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            Description = "Electronic devices and accessories",
                            Name = "Electronics"
                        },
                        new
                        {
                            CategoryId = new Guid("e5f6a7b8-5678-9101-2345-6789abcdef12"),
                            CreatedAt = new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            Description = "Men's and women's clothing",
                            Name = "Clothing"
                        },
                        new
                        {
                            CategoryId = new Guid("f6a7b8c9-6789-1011-3456-789abcdef123"),
                            CreatedAt = new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            Description = "Home appliances and kitchenware",
                            Name = "Home & Kitchen"
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("ShippingPostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("Pending");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("ShippingPostalCode");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"),
                            CreatedAt = new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ShippingPostalCode = "380051",
                            Status = "Delivered",
                            TotalAmount = 849.97m,
                            UpdatedAt = new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567")
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("8b9c0d1e-8901-1213-4567-89abcdef1234"),
                            OrderId = new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"),
                            ProductId = new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"),
                            Quantity = 1,
                            UnitPrice = 799.99m
                        },
                        new
                        {
                            OrderItemId = new Guid("9c0d1e2f-9012-1314-5678-9abcdef12345"),
                            OrderId = new Guid("7a8b9c0d-7890-1112-3456-789abcdef123"),
                            ProductId = new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"),
                            Quantity = 2,
                            UnitPrice = 24.99m
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.OrderStatusLog", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ChangedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("ChangedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("OldStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LogId");

                    b.HasIndex("ChangedBy");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderStatusLogs");

                    b.HasData(
                        new
                        {
                            LogId = new Guid("0d1e2f3a-0123-1415-6789-abcdef123456"),
                            ChangedAt = new DateTime(2023, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            ChangedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            NewStatus = "Accepted",
                            OldStatus = "Pending",
                            OrderId = new Guid("7a8b9c0d-7890-1112-3456-789abcdef123")
                        },
                        new
                        {
                            LogId = new Guid("1e2f3a4b-1234-1516-7890-bcdef1234567"),
                            ChangedAt = new DateTime(2023, 5, 2, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            ChangedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            NewStatus = "Delivered",
                            OldStatus = "Accepted",
                            OrderId = new Guid("7a8b9c0d-7890-1112-3456-789abcdef123")
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("SKU")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("1a2b3c4d-1234-5678-9012-abcdef123456"),
                            CategoryId = new Guid("d4e5f6a7-4567-8910-1234-56789abcdef1"),
                            CreatedAt = new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            Description = "Latest smartphone with advanced features",
                            ImageUrl = "images/products/smartphone-x.jpg",
                            Name = "Smartphone X",
                            Price = 799.99m,
                            SKU = "SPX-1001",
                            StockQuantity = 100
                        },
                        new
                        {
                            ProductId = new Guid("2b3c4d5e-2345-6789-0123-bcdef1234567"),
                            CategoryId = new Guid("d4e5f6a7-4567-8910-1234-56789abcdef1"),
                            CreatedAt = new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            Description = "Noise cancelling wireless headphones",
                            ImageUrl = "images/products/headphones.jpg",
                            Name = "Wireless Headphones",
                            Price = 199.99m,
                            SKU = "WH-2002",
                            StockQuantity = 50
                        },
                        new
                        {
                            ProductId = new Guid("3c4d5e6f-3456-7890-1234-cdef12345678"),
                            CategoryId = new Guid("e5f6a7b8-5678-9101-2345-6789abcdef12"),
                            CreatedAt = new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedBy = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            Description = "Premium quality cotton t-shirt",
                            ImageUrl = "images/products/tshirt.jpg",
                            Name = "Cotton T-Shirt",
                            Price = 24.99m,
                            SKU = "CT-3003",
                            StockQuantity = 200
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.ShoppingCart", b =>
                {
                    b.Property<Guid>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");

                    b.HasData(
                        new
                        {
                            CartId = new Guid("4d5e6f7a-4567-8910-1234-56789abcdef1"),
                            CreatedAt = new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567")
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PostalCode");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a1b2c3d4-1234-5678-9012-abcdef123456"),
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@shoppingportal.com",
                            Firstname = "Admin",
                            IsActive = true,
                            Lastname = "User",
                            PasswordHash = "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=",
                            PhoneNumber = "1234567890",
                            PostalCode = "393120",
                            StreetAddress = "123 Admin Street",
                            UserType = "Admin"
                        },
                        new
                        {
                            UserId = new Guid("b2c3d4e5-2345-6789-0123-bcdef1234567"),
                            CreatedAt = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.doe@example.com",
                            Firstname = "John",
                            IsActive = true,
                            Lastname = "Doe",
                            PasswordHash = "ZlKyJQ6uc/cQ0FZNsH/AqjoLvXeATZrliDoFdEMm6pk=",
                            PhoneNumber = "2345678901",
                            PostalCode = "380051",
                            StreetAddress = "456 Customer Ave",
                            UserType = "Customer"
                        });
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.CartItem", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingPortal.Data.Entities.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Category", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.User", "CreatedByUser")
                        .WithMany("CategoriesCreated")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Order", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.Address", "ShippingAddress")
                        .WithMany("Orders")
                        .HasForeignKey("ShippingPostalCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShoppingPortal.Data.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ShippingAddress");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingPortal.Data.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.OrderStatusLog", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.User", "ChangedByUser")
                        .WithMany("StatusChangesMade")
                        .HasForeignKey("ChangedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShoppingPortal.Data.Entities.Order", "Order")
                        .WithMany("StatusLogs")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingPortal.Data.Entities.Product", "Product")
                        .WithMany("StatusLogs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ChangedByUser");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Product", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShoppingPortal.Data.Entities.User", "CreatedByUser")
                        .WithMany("ProductsCreated")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.ShoppingCart", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.User", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("ShoppingPortal.Data.Entities.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.User", b =>
                {
                    b.HasOne("ShoppingPortal.Data.Entities.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("PostalCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Address", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("StatusLogs");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderItems");

                    b.Navigation("StatusLogs");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.ShoppingCart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("ShoppingPortal.Data.Entities.User", b =>
                {
                    b.Navigation("CategoriesCreated");

                    b.Navigation("Orders");

                    b.Navigation("ProductsCreated");

                    b.Navigation("ShoppingCart")
                        .IsRequired();

                    b.Navigation("StatusChangesMade");
                });
#pragma warning restore 612, 618
        }
    }
}
