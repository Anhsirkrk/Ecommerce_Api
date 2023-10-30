using Microsoft.Identity.Client;

namespace Ecommerce_Api.ViewModels
{
    public class UserSubscriptionProductsViewModel
    {

        public int ItemId { get; set; }
        public int OrderId { get; set; }    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }

        public decimal productindividualprice { get; set; }

        public string SubscriptionType { get; set; }

        public string image { get; set; }

        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Pincode { get; set; }
        public string HouseNo { get; set; }

        public string paymentrefno { get; set; }
       



        public string ProductDescription { get; set; }
        
        public decimal SizeOfEachUnit { get; set; }
        public decimal ProductPrice { get; set; }
       
       
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }

}
