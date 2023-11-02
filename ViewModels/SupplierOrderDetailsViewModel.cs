namespace Ecommerce_Api.ViewModels
{
    public class SupplierOrderDetailsViewModel
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public string DeliveryAddress { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string SubscriptionTypes { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
    }
}
