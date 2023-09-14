namespace Ecommerce_Api.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string ProductName { get; set; } = null!;

        public int StockQuantity { get; set; }

        public decimal Price { get; set; }

        public decimal Weight { get; set; }

        public string Unit { get; set; }

        public string ImageUrl { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufactureDate { get; set; }

        public int DiscountId { get; set; }

        public string Description { get; set; }
    }
}
