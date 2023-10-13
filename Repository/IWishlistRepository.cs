using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Model;
namespace Ecommerce_Api.Repository
{
    public interface IWishlistRepository
    {
        Task<WishlistViewModel> AddWishlistOfProductbyUserID(WishlistViewModel wishlistViewModel);
        Task<List<UserWishlistViewModel>> GetUserWishlistProducts(int userid);
    }
}
