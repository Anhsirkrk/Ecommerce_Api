using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EcommercedemoContext context;
        private readonly IUserRepository iur;
        public UserController(IUserRepository _iur, EcommercedemoContext _context)
        {
            context = _context;
            iur = _iur;
            context.Database.SetCommandTimeout(120);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserViewModel userviewmodel)
        {
            try
            {
                var item = await iur.CreateUser(userviewmodel);
                if (item == null)
                {
                    return BadRequest("User Not Created");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetProductsByBrand")]
        public async Task<List<TotalViewModel>> GetProductsByBrand(int brand_Id)
        {
            try
            {
                if (brand_Id != 0)
                {
                    return await iur.GetProductsByBrand(brand_Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUserSubscribedProducts")]
        public async Task<ActionResult<List<UserSubscriptionProductsViewModel>>> GetUserSubsriptionProductsBasedonUserId(int userId)
        {
            try
            {
           
                if (userId != 0)
                {
                    
                    var item= await iur.GetUserSubsriptionProductsBasedonUserId(userId);
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
