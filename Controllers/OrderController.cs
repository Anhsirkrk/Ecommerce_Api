using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository ior;
        private readonly EcommerceDailyPickContext context;

        public OrderController(IOrderRepository _ior, EcommerceDailyPickContext _context)
        {
            context = _context;
            ior = _ior;
        }


        [Authorize(Roles = "1,2")]
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        
        {
            if (model == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var result = await ior.AddOrderOfUser(model);

                // Create a custom response
                var customOrder = new
                {
                    Message = "Order Placed successfully",
                    Supplier = result // Include the newly created supplier data
                };

                return Ok(customOrder);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an appropriate response.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}