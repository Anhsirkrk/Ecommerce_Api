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
        public async Task<IActionResult> CreateCartForUser(CartViewModel cvm)
        {
            try
            {
                if (context != null && cvm!= null)
                {
                    var newcart = await icr.CreateCartForUser(cvm);
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
