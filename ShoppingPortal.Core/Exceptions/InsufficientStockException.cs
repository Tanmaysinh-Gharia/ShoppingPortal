using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public string ProductName { get; }
        public int AvailableQuantity { get; }

        public InsufficientStockException(string productName, int availableQuantity)
            : base($"{productName} only has {availableQuantity} items left in stock")
        {
            ProductName = productName;
            AvailableQuantity = availableQuantity;
        }
    }
}
