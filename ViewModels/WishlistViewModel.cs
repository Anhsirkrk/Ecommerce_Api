namespace Ecommerce_Api.ViewModels
{
    public class WishlistViewModel
    {
        public int WishlistId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public bool IsInWishlist { get; set; }

    }
}