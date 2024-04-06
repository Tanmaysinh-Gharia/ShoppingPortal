using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.DTOs.Admin
{
    public class AdminDashboardDto
    {
        public int CategoryCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public List<AdminActivityDto> RecentActivities { get; set; }
    }

    public class AdminActivityDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string IconClass { get; set; }
    }
}
