using Ecommerce_Api.Model;
using Ecommerce_Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly EcommercedemoContext context;
        public ReviewRepository(EcommercedemoContext _context)
        {
            context = _context;
        }

        //adding rating of product 
       public async Task<ReviewViewModel> AddReviewOfProduct(ReviewViewModel reviewViewModel)
        {
            try
            {
                var review = new Review
                {
                    UserId = reviewViewModel.UserId,
                    ProductId = reviewViewModel.ProductId,
                    Rating = reviewViewModel.Rating,
                    Comment = reviewViewModel.Comment,
                    ReviewDate = DateTime.UtcNow
                };

                context.Reviews.Add(review);
                await context.SaveChangesAsync();
                var viewViewModel = new ReviewViewModel
                {
                    ReviewId = review.ReviewId,
                    UserId = (int)review.UserId,
                    ProductId = (int)review.ProductId,
                    Rating = (int)review.Rating,
                    Comment = review.Comment,
                    //ReviewDate = DateTime.UtcNow
                };

                return viewViewModel;
            }
            catch
            {
                throw;
            }
           
        }

        //getting product review details based on productid
        public async Task<List<ProductReviewsViewModel>> GetProductReviewsBasedOnProductId(int productId)
        {
            var reviews = await context.Reviews
           .Where(r => r.ProductId == productId)
           .Select(r => new ProductReviewsViewModel
           {
               UserName = r.User.Username, // Assuming you have a 'User' navigation property in your 'Review' class
               ProductId = (int)r.ProductId,
               Rating = r.Rating ?? 0, // Handle nullable 'Rating' property
               Comment = r.Comment
           })
           .ToListAsync();

            return reviews;
        }
    }
}
