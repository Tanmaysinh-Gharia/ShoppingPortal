﻿using ShoppingPortal.Core.Enums;
using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Seeds
{
    public static class UserSeedData
    {
        public static User[] GetSeedUsers()
        {
            return new User[]
            {
                new User
                {
                UserId = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"),
                Email = "admin@shoppingportal.com",
                PasswordHash = PasswordHasher.HashPassword("Admin@123"),
                Firstname = "Admin",
                Lastname = "User",
                PhoneNumber = "1234567890",
                CreatedAt = new DateTime(2023, 1, 1),
                IsActive = true,
                UserType = UserTypeEnum.Admin,
                StreetAddress = "123 Admin Street",
                PostalCode = "393120"
                },

                new User
                {
                    UserId = Guid.Parse("b2c3d4e5-2345-6789-0123-bcdef1234567"),
                    Email = "john.doe@example.com",
                    PasswordHash = PasswordHasher.HashPassword("JohnDoe@123"),
                    Firstname = "John",
                    Lastname = "Doe",
                    PhoneNumber = "2345678901",
                    CreatedAt = new DateTime(2023, 1, 15), // Static date
                    IsActive = true,
                    UserType = UserTypeEnum.Customer,
                    StreetAddress = "456 Customer Ave",
                    PostalCode = "380051"
                },
                new User
                {
                    UserId = Guid.Parse("a2c3d4e5-2345-6789-0123-bcdef1234567"),
                    Email = "tanmaysinh.gharia@gmail.com",
                    PasswordHash = PasswordHasher.HashPassword("Tanmaysinh@123"),
                    Firstname = "Tanmaysinh",
                    Lastname = "Gharia",
                    PhoneNumber = "1234567890",
                    CreatedAt = new DateTime(2025, 2, 2), // Static date
                    IsActive = false,
                    UserType = UserTypeEnum.Customer,
                    StreetAddress = "JV Park",
                    PostalCode = "380051"
                },
                new User
                {
                    UserId = Guid.Parse("b1c3d4e5-2345-6789-0123-bcdef1234567"),
                    Email = "tanmay.sinh@gmail.com",
                    PasswordHash = PasswordHasher.HashPassword("Tanmaysinh@123"),
                    Firstname = "Tanmaysinh",
                    Lastname = "Gharia",
                    PhoneNumber = "1234567890",
                    CreatedAt = new DateTime(2025, 2, 2), // Static date
                    IsActive = true,
                    UserType = UserTypeEnum.Customer,
                    StreetAddress = "JV Park",
                    PostalCode = "380051"
                },

                new User
                {
                    UserId = Guid.Parse("d2c3d4e5-2345-6789-0123-bcdef1234567"),
                    Email = "boom.baam@gmail.com",
                    PasswordHash = PasswordHasher.HashPassword("BoomBaam@123"),
                    Firstname = "Boom",
                    Lastname = "Baam",
                    PhoneNumber = "1234562890",
                    CreatedAt = new DateTime(2025, 2, 1), // Static date
                    IsActive = false,
                    UserType = UserTypeEnum.Customer,
                    StreetAddress = "JV Park",
                    PostalCode = "380051"
                }
        };
    }
    }
}
