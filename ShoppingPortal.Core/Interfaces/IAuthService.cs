using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticatedUserResponse> AuthenticateAsync(LoginRequest request);
        Task<UserRegistrationResponse> RegisterAsync(RegisterRequest request);
    }
}
