using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IUserRepository iur;
        public UserController(IUserRepository _iur, EcommerceDailyPickContext _context)
        {
            context = _context;
            iur = _iur;
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
        [Route("GetUserSubscribedProducts")]
        public async Task<ActionResult<List<UserSubscriptionProductsViewModel>>> GetUserSubsriptionProductsBasedonUserId(int userId)
        {
            try
            {

                if (userId != 0)
                {

                    var item = await iur.GetUserSubsriptionProductsBasedonUserId(userId);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetUserDetailsByUserId")]
        public async Task<ActionResult<User>> GetUserDetailsByUserId(int userid)
        {
            try
            {

                if (userid != 0)
                {

                    var item = await iur.GetUserDetailsByUserId(userid);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UpdateUserDetails")]
        public async Task<ActionResult<UserViewModel>> UpdateUserDetails(UserViewModel user)
        {
            try
            {

                if (user.UserId != 0)
                {

                    var item = await iur.UpdateUserDetails(user);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AddingAdressDetails")]
        public async Task<ActionResult<UserAdressViewModel>> AddingAdressDetails(UserAdressViewModel address)
        {
            try
            {

                if (address != null)
                {

                    var item = await iur.AddingAdressDetails(address);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTheUserAdressDetails")]
        public async Task<ActionResult<List<UserAdressViewModel>>> GetTheUserAdressDetails(int userid)
        {
            try
            {
                if (userid != 0)
                {
                    var item = await iur.GetTheUserAdressDetails(userid);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        //hidden
        //[HttpGet]
        //[Route("GetProductsByBrand")]
        //public async Task<List<TotalViewModel>> GetProductsByBrand(int brand_Id)
        //{
        //    try
        //    {
        //        if (brand_Id != 0)
        //        {
        //            return await iur.GetProductsByBrand(brand_Id);
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




    }
}
