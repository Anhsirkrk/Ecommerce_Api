using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IReviewRepository irr;
        public ReviewsController(IReviewRepository _irr, EcommerceDailyPickContext _context)
        {
            context = _context;
            irr = _irr;
            context.Database.SetCommandTimeout(120);
        }


        [Authorize(Roles = "1,2")]
        [HttpPost]
        [Route("AddReviewOfProduct")]
        public async Task<ActionResult<ReviewViewModel>> AddReviewOfProduct(ReviewViewModel reviewViewModel)
        {
            try
            {

                if (context != null)
                {

                    var item = await irr.AddReviewOfProduct(reviewViewModel);
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //getting product review details based on productid

        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("GetProductReviewsBasedOnProductId")]
        public async Task<ActionResult<List<ProductReviewsViewModel>>> GetProductReviewsBasedOnProductId(int productId)
        {
            try
            {

                if (productId != 0)
                {

                    var item = await irr.GetProductReviewsBasedOnProductId(productId);
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("GetAverageRatingForProduct")]
        public async Task<ActionResult<double>> GetAverageRatingForProduct(int productId)
        {
            try
            {

                if (productId != 0)
                {

                    var item = await irr.GetAverageRatingForProduct(productId);
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

