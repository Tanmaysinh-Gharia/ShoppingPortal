using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
//using ShoppingPortal.Web.Models;
using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Interfaces;
using ShoppingPortal.Core.DTOs;

namespace ShoppingPortal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region LOG IN

        // LOG IN
        public IActionResult Login()
        {
            ViewData["HideNavbar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model, string returnUrl = null)
        {

            ViewData["HideNavbar"] = true;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LoginRole loginrole = await _userService.ValidateUserCredentialsAsync(model);

            // State is valid but user is not valid
            if (loginrole == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username/email or password");
                return View(model);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginrole.UserId.ToString()),
                new Claim(ClaimTypes.Name, loginrole.Username),
                new Claim(ClaimTypes.Email, loginrole.Email),
                new Claim(ClaimTypes.Role, loginrole.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7) 
                });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            switch (loginrole.Role)
            {
                case "Admin":
                    return RedirectToAction("Index", "Admin");

                case "Customer":
                    return RedirectToAction("Index", "Product");

                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        #endregion


    }
}
