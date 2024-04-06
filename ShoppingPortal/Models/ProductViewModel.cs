namespace ShoppingPortal.Web.Models
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public List<Guid> SelectedCategoryIds { get; set; } = new();
    }
}
