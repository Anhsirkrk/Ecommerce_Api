using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly EcommercedemoContext context;
        private readonly ICartInterface icr;

        public CartController(EcommercedemoContext _context,ICartInterface _icr)
        {
            context = _context;
            icr = _icr;
        }

        [HttpPost]
        [Route("CreateCart")]
        public async Task<IActionResult> AddItemToCart(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var newcart = await icr.AddItemToCart(cvm);
                    if (newcart.IsItemAdded==true)
                    {
                        return (IActionResult)newcart;
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
            return BadRequest("Item Not Added");
        }

        [HttpPost]
        [Route("ChangingQuantityOfItem")]
        public async Task<IActionResult> ChangingQuantityOfItem(CartViewModel cvm)
        {
            try
            {
                if(context != null && cvm != null)
                {
                    var updateproduct = await icr.ChangingQuantityOfItem(cvm);
                    if (updateproduct.IsQuantityUpdated == true)
                    {
                        return (IActionResult)updateproduct;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return BadRequest("Item Not Updated");

        }

        [HttpPost]
        [Route("DeletecartItem")]
        public async Task<IActionResult> DeletecartItem(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var deleteproduct = await icr.DeletecartItem(cvm);
                    if (deleteproduct.IsItemDeleted == true)
                    {
                        return (IActionResult)deleteproduct;
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
            return BadRequest("Item Not Updated");

        }


        [HttpPost]
        [Route("CreateCartForUserMultipleProductsAtOnce")]
        public async Task<IActionResult> CreateCartForUserMultipleProductsAtOnce(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm!= null)
                {
                    var newcart = await icr.CreateCartForUserMultipleProductsAtOnce(cvm);
                    if (newcart != null)
                    {
                        return (IActionResult)newcart;
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BadRequest();
        }

    }
}
