using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Constants
{
    public static class ProductConstants
    {

        public const int MaxQuantityPerProduct = 10; // Maximum quantity user can select
        public const int MaxItemsInCart = 20; // Maximum different products in cart
        public const int MinQuantityPerProduct = 1; // Minimum quantity user can select
        public const string QuantityExceedsLimit = "Quantity exceeds the maximum allowed limit";
        public const string CartFullMessage = "Your cart has reached the maximum item limit";
    }
}
