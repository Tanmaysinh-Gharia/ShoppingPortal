using Microsoft.EntityFrameworkCore;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Data.Context;
using ShoppingPortal.Data.Interfaces;
using ShoppingPortal.Data.Repositories;
//using ShoppingPortal.Services.User;
using ShoppingPortal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPortal.Data.Entities;
using System.Security.Cryptography;
using ShoppingPortal.Core.Helpers;
using AutoMapper;

namespace ShoppingPortal.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) 
        { 
            _userRepository = userRepository;
            _mapper = mapper;
        }



        #region Login Services
        public async Task<LoginRole> ValidateUserCredentialsAsync(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) ||
                string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return null;
            }

            LoginRole loginrole;

            loginrole = _mapper.Map<LoginRole>(await _userRepository.GetByEmailAsync(loginDto.Email));

            if (loginrole == null || !loginrole.IsActive || !VerifyPasswordHash(loginDto.Password, loginrole.PasswordHash))
            {
                return null;
            }

            return loginrole;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // In a real application, you would use a proper password hashing algorithm
            // like PBKDF2, BCrypt, or Argon2. This is a simplified example.

            return PasswordHasher.VerifyPassword(password, storedHash);
        }




        #endregion















        // Example using eager loading
        //public async Task<UserDto> GetUserWithOrdersAsync(Guid userId)
        //{
        //    var user = await GetUserWithEagerLoading(userId)
        //        .Select(u => new UserDto
        //        {
        //            // mapping
        //        })
        //        .FirstOrDefaultAsync();

        //    return user;
        //}

        // Example using explicit loading
        //public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
        //{
        //    var user = await GetUserWithExplicitLoadingAsync(userId, loadAddress: true);
        //    return MapToProfileDto(user);
        //}

        // Example using lazy loading
        //public UserDto GetUserWithLazyLoadedData(Guid userId)
        //{
        //    return GetUserWithLazyLoading(userId);
        //}   
    }
}
