using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface ICartRepository
    {
        Task<CartViewModel> AddItemToCart(CartViewModel cvm);
        Task<CartViewModel> DeletecartItem(CartViewModel cvm);
        Task<CartViewModel> ChangingQuantityOfItem(CartViewModel cvm);
        Task<CartViewModel> CreateCartForUserMultipleProductsAtOnce(CartViewModel cartviewmodel);
 
    }
}
