﻿using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Interfaces
{
    public interface IUserRepository
    {

        //Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);

        Task<string> GetPostalCodeOfUser(Guid userId);
    }
}
