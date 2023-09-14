using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface ICartInterface
    {
        Task<CartViewModel> CreateCartForUser(CartViewModel cartviewmodel);
    }
}
