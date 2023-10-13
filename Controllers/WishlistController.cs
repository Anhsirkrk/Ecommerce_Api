using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {

        private readonly EcommercedemoContext context;
        private readonly IWishlistRepository iwr;
        public WishlistController(IWishlistRepository _iwr, EcommercedemoContext _context)
        {
            context = _context;
            iwr = _iwr;
            context.Database.SetCommandTimeout(120);
        }

        [HttpPost]
        [Route("CreateWishlist")]
        public async Task<IActionResult> CreateWishlist(WishlistViewModel wishlistViewModel)
        {
            try
            {
                var item = await iwr.AddWishlistOfProductbyUserID(wishlistViewModel);
                if (item == null)
                {
                    return BadRequest("Wishlist Not Created");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetUserWishlistProducts")]
        public async Task<ActionResult<List<UserWishlistViewModel>>> GetUserWishlistProducts(int userid)
        {
            try
            {

                if (userid != 0)
                {

                    var item = await iwr.GetUserWishlistProducts(userid);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
