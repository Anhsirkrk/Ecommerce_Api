namespace Ecommerce_Api.ViewModels
{
    public class TotalViewModel
    {
        //vendor

        public int VendorId { get; set; }

        public string NameofVendor { get; set; }

        public string Address { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string Description { get; set; }

        public string LogoUrl { get; set; }

        //usertype

        public int TypeId { get; set; }

        public string UserType1 { get; set; }

        //usersubscription
        public int UserSubscriptionId { get; set; }

        public int UserId { get; set; }

        public int SubscriptionTypeId { get; set; }

        public int OrderId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal SubscriptionPrice { get; set; }

        public bool IsActive { get; set; }

        //usercoupon

        public int UserCouponId { get; set; }

        public int CouponId { get; set; }

        public DateTime UsageDate { get; set; }

        //user

       

        public int UserTypeId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public decimal Mobile { get; set; }

        public string Email { get; set; } = null!;

        

        //subscriptiontype

        public int SubscriptionId { get; set; }

        public string SubscriptionType1 { get; set; } = null!;

        //ShoppingCartitem
        public int ItemId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        //shoppingcart
        
        public DateTime CreatedAt { get; set; }

        //product
        

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string ProductName { get; set; } = null!;

        public int StockQuantity { get; set; }

       

        public decimal Weight { get; set; }

        public string Unit { get; set; }

        public string ImageUrl { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime ManufactureDate { get; set; }

        public int DiscountId { get; set; }

       

        //Review

        public int ReviewId { get; set; }

        

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        //payment
        public int PaymentId { get; set; }

        

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public decimal Amount { get; set; }

        public string TransactionId { get; set; } = null!;

        //orderitem
        
        public string SubscriptionType { get; set; } = null!;

        
        public decimal ProductPrice { get; set; }

        //order
       

        

        

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

       

        //log

        public int LogId { get; set; }

        public DateTime LogDate { get; set; }

        public string EventDescription { get; set; }

       

        //discount


       

        public decimal DiscountPercentage { get; set; }

        


        //coupon
        

        public string Code { get; set; }

        public decimal Discount { get; set; }

       

        //category

        

        public string CategoryName { get; set; } = null!;

        

        //brand

        

        public string BrandName { get; set; } = null!;



    }
}
