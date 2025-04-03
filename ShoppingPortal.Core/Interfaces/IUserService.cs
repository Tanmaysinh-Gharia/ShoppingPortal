using ShoppingPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Interfaces
{
    public interface IUserService 
    {
        //Task<UserDto> RegisterAsync(RegisterDto registerDto);
        #region Login Services

        Task<LoginRole> ValidateUserCredentialsAsync(LoginDto loginDto);
        //Task<UserDto> LoginAsync(LoginDto loginDto);

        //#endregion

        //#region Helper Services
        //Task<UserDto> GetUserByIdAsync(Guid userId);
        //Task<bool> VerifyUserAsync(Guid userId);

        #endregion
    }
}
