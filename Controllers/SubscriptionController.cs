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
    public class SubscriptionController : ControllerBase
    {

        private readonly EcommercedemoContext context;
        private readonly ISubscriptionRepository isr;
        public SubscriptionController(ISubscriptionRepository _isr, EcommercedemoContext _context)
        {
            context = _context;
            isr = _isr;
            context.Database.SetCommandTimeout(120);
        }

        [HttpGet]
        [Route("GetSubscriptionTypes")]
        public async Task<ActionResult<List<SubscriptionType>>> GetSubscriptionTypes()
        {
            try
            {

                if (context != null)
                {

                    var item = await isr.GetSubscriptionTypes();
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ;
            }
           
        }
    }
}
