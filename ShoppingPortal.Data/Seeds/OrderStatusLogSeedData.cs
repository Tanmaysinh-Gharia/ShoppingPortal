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
    public static class OrderStatusLogSeedData
    {
        public static OrderStatusLog[] GetSeedOrderStatusLogs()
        {

            return new OrderStatusLog[]
                {
                new OrderStatusLog
                {
                    LogId = Guid.Parse("0d1e2f3a-0123-1415-6789-abcdef123456"),
                    OrderId = Guid.Parse("7a8b9c0d-7890-1112-3456-789abcdef123"), // John's order
                    OldStatus = OrderStatusEnum.Pending,
                    NewStatus = OrderStatusEnum.Accepted,
                    ChangedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    ChangedAt = new DateTime(2023, 5, 1, 10, 0, 0)
                },
                new OrderStatusLog
                {
                    LogId = Guid.Parse("1e2f3a4b-1234-1516-7890-bcdef1234567"),
                    OrderId = Guid.Parse("7a8b9c0d-7890-1112-3456-789abcdef123"), // John's order
                    OldStatus = OrderStatusEnum.Accepted,
                    NewStatus = OrderStatusEnum.Delivered,
                    ChangedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    ChangedAt = new DateTime(2023, 5, 2, 15, 0, 0)
                }
                };
        }
    }
}
