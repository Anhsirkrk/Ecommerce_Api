namespace Ecommerce_Api.ViewModels
{
    public class ReviewViewModel
    {
        public int ReviewId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public decimal Rating { get; set; }

        public string Comment { get; set; }

        //public DateTime ReviewDate { get; set; }
    }
}
