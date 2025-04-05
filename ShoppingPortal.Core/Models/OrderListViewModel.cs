using ShoppingPortal.Core.DTOs;
using ShoppingPortal.Core.Models;

namespace ShoppingPortal.Core.Models
{
    public class OrderListViewModel
    {

        public List<OrderDto> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
