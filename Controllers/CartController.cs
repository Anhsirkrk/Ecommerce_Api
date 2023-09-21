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
        private readonly ICartRepository icr;

        public CartController(EcommercedemoContext _context,ICartRepository _icr)
        {
            context = _context;
            icr = _icr;
        }

        [HttpPost]
        [Route("AddItemToCart")]
        public async Task<CartViewModel> AddItemToCart(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm != null)
                {
                    var newcart = await icr.AddItemToCart(cvm);
                    if (newcart.IsItemAdded==true)
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

        [HttpPost]
        [Route("ChangingQuantityOfItem")]
        public async Task<CartViewModel> ChangingQuantityOfItem(CartViewModel cvm)
        {
            try
            {
                if(context != null && cvm != null)
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
            catch(Exception ex)
            {
                throw ex;
            }
            cvm.Resultmessage = "Item Not Updated";
            return cvm;

        }

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


        [HttpPost]
        [Route("CreateCartForUserMultipleProductsAtOnce")]
        public async Task<CartViewModel> CreateCartForUserMultipleProductsAtOnce(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm!= null)
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

    }
}
