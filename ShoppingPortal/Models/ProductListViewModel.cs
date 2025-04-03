using ShoppingPortal.Core.DTOs;

namespace ShoppingPortal.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
