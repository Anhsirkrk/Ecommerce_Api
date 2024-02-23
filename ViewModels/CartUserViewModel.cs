namespace Ecommerce_Api.ViewModels
{
    public class CartUserViewModel
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public string CategoryName { get; set; }

        public string BrandName { get; set; }

    

        public string ProductName { get; set; } = null!;


        public string Base64Image { get; set; }

        public string Unit { get; set; }

        public List<decimal> SizeOfEachUnits { get; set; }

        public List<decimal> WeightOfEachUnits { get; set; }

        public List<decimal> StockOfEachUnits { get; set; }

        public List<decimal> PriceOfEachUnits { get; set; }

        public List<DateTime> MFG_OfEachUnits { get; set; }
        public List<DateTime> EXP_OfEachUnits { get; set; }
        public List<bool> IsAvailable_OfEachUnit { get; set; }

        public List<decimal> Avaialble_Quantity_ofEachUnit { get; set; }
        public List<string> Description_OfEachUnits { get; set; }

        public List<int> DiscountId_OfEachUnit { get; set; }

        //public string ResultMessage { get; set; }
        public bool IsAvailable { get; set; }

        public decimal SelectedSizeOfItem { get; set; }

        public decimal SelectedQuantityofItem { get; set; }

        public decimal WeightOfUnit { get; set; }

        public decimal StockOfUnit { get; set; }
        public DateTime MFG { get; set; }
        public DateTime EXP { get; set; }

        public decimal Price { get; set; }
        public int DiscountId { get; set; }
        public decimal Avaialble_Quantity { get; set; }
        public string Description { get; set; }

        public decimal SizeOfUnit { get; set; }
        public decimal selectedSizeOfUnit { get; set; }
       // public int selectedquantityofitem { get; set; }
    }
}
