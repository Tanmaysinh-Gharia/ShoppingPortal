using ShoppingPortal.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.Extentions
{
    public static class EnumExtensions
    {
        public static string ToDisplayString(this OrderStatusEnum status)
        {
            return status switch
            {
                _ => status.ToString()
            };
        }

        public static string ToDisplayString(this UserTypeEnum userType)
        {
            return userType switch
            {
                _ => userType.ToString()
            };
        }

        public static string GetStatusColor(this OrderStatusEnum status)
        {
            return status switch
            {
                OrderStatusEnum.Pending => "#87CEEB",       // Skyblue
                OrderStatusEnum.Accepted => "#FFA500",    // Orange
                OrderStatusEnum.Shipped => "#4169E1",       // Royalblue
                OrderStatusEnum.Delivered => "#32CD32",     // Limegreen
                OrderStatusEnum.Cancelled => "#A9A9A9",     // Darkgray
                OrderStatusEnum.Returned => "#FF6347", // Tomato
                _ => "#FFFFFF"                              // Default white
            };
        }

        public static string GetStatusBadgeClass(this OrderStatusEnum status)
        {
            return status switch
            {
                OrderStatusEnum.Pending => "bg-warning",
                OrderStatusEnum.Accepted => "bg-info",
                OrderStatusEnum.Shipped => "bg-primary",
                OrderStatusEnum.Delivered => "bg-success",
                OrderStatusEnum.Cancelled => "bg-secondary",
                OrderStatusEnum.Returned=> "bg-danger",
                _ => "bg-light"
            };
        }
        // Generic version using Description attribute if present

    }
}
