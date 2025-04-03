using ShoppingPortal.Core.Enums;
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
    public static class OrderSeedData
    {
        public static Order[] GetSeedOrders()
        {
            return new Order[]
            {
                new Order
                {
                    OrderId = Guid.Parse("7a8b9c0d-7890-1112-3456-789abcdef123"),
                    UserId = Guid.Parse("b2c3d4e5-2345-6789-0123-bcdef1234567"), // John Doe
                    Status = OrderStatusEnum.Delivered,
                    TotalAmount = 849.97m,
                    ShippingPostalCode = "380051", // John's address
                    CreatedAt = new DateTime(2023, 5, 1),
                    UpdatedAt = new DateTime(2023, 5, 2)
                }
            };
        }
    }
}
