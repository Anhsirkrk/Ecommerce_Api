using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
namespace Ecommerce_Api.Repository
{
    public interface IReviewRepository
    {
        Task<ReviewViewModel> AddReviewOfProduct(ReviewViewModel reviewViewModel);
        Task<List<ProductReviewsViewModel>> GetProductReviewsBasedOnProductId(int productId);
    }
}
