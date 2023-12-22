using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Twilio.Jwt.Taskrouter;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly EcommerceDailyPickContext context;
        private readonly ICartRepository icr;

        public CartController(EcommerceDailyPickContext _context, ICartRepository _icr)
        {
            context = _context;
            icr = _icr;
            context.Database.SetCommandTimeout(120);
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        [Route("AddItemToCart")]
        public async Task<CartViewModel> AddItemToCart(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var newcart = await icr.AddItemToCart(cvm);
                    if (newcart.IsItemAdded == true)
                    {
                        return newcart;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cvm.Resultmessage = "ITEM NOT ADDED";
            return cvm;
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        [Route("ChangingQuantityOfItem")]
        public async Task<CartViewModel> ChangingQuantityOfItem(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var updateproduct = await icr.ChangingQuantityOfItem(cvm);
                    if (updateproduct.IsQuantityUpdated == true)
                    {
                        return updateproduct;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cvm.Resultmessage = "Item Not Updated";
            return cvm;

        }


        [Authorize(Roles = "2")]
        [HttpPost]
        [Route("DeletecartItem")]
        public async Task<CartViewModel> DeletecartItem(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var deleteproduct = await icr.DeletecartItem(cvm);
                    if (deleteproduct.IsItemDeleted == true)
                    {
                        return deleteproduct;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cvm.Resultmessage = "Item Not Updated";
            return cvm;

        }

        [Authorize(Roles = "2")]
        [HttpPost]
        [Route("CreateCartForUserMultipleProductsAtOnce")]
        public async Task<CartViewModel> CreateCartForUserMultipleProductsAtOnce(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var newcart = await icr.CreateCartForUserMultipleProductsAtOnce(cvm);
                    if (newcart != null)
                    {
                        return newcart;
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            cvm.Resultmessage = "not inserted to cart";
            return cvm;
        }

        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("GetCartItemsBasedOnUserId")]
        public async Task<ActionResult<List<CartUserViewModel>>> GetCartItemsBasedOnUserId(int userId)
        {
            try
            {

                if (userId != 0)
                {

                    var item = await icr.GetCartItemsBasedOnUserId(userId);
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
