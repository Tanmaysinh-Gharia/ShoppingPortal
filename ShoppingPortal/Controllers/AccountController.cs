using Microsoft.AspNetCore.Mvc;
using ShoppingPortal.Services.User;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingPortal.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController (IUserService userService)
        {
            _userService = userService;
        }

        #region LOG IN

        // LOG IN
        

        #endregion


    }
}
