using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;

namespace Ecommerce_Api.Repository
{
    public interface IOrderRepository
    {
        Task<OrderViewModel> AddOrderOfUser(OrderViewModel orderViewModel);
    }
}