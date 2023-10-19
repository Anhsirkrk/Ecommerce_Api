namespace Ecommerce_Api.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }


        public decimal SizeOfProduct { get; set; }
        public int SubscriptionTypeId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OrderPaymentStatus { get; set; }

        public string TimeSlot { get; set; }

        public int AddressId { get; set; }

        public int SupplierId { get; set; }
       
    }
}
