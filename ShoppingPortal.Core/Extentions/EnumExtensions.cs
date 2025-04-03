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

        // Generic version using Description attribute if present
        
    }
}
