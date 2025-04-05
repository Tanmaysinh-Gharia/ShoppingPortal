using ShoppingPortal.Core.DTOs;

namespace ShoppingPortal.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }


        // Search, sort , filter properties
        // Add filter/sort properties
        public string SearchTerm { get; set; }
        public string SortBy { get; set; } = "name";
        public bool SortAsc { get; set; } = true;
        public Guid? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStock { get; set; }
    }
}
