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
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        //private readonly IShoppingCartService _shoppingCartService;
        //private readonly IOrderService _orderService;
        //private readonly IProductService _productService;
        //private readonly ICategoryService _categoryService;
        //private readonly IAddressService _addressService;

        public CustomerController(IUserService userService)
        {
            _userService = userService;
        }

        #region LOG IN

        // LOG IN
        
        #endregion


    }
}
