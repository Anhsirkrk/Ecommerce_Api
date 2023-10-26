namespace Ecommerce_Api.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }
        public int UserSubscriptionId { get; set; }

        public int OrderId { get; set; }

        //public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public decimal Amount { get; set; }

        //public string TransactionId { get; set; } = null!;

        //public string PaymentStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }   

    }
}
