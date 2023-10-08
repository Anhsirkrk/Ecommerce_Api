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
    public class PaymentController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IPaymentRepository ipr;
        public PaymentController(IPaymentRepository _ipr, EcommerceDailyPickContext _context)
        {
            context = _context;
            ipr = _ipr;
            context.Database.SetCommandTimeout(120);
        }

        [HttpPost]
        [Route("UserPayment")]
        public async Task<ActionResult<PaymentViewModel>> UserPayment(PaymentViewModel paymentView)
        {
            try
            {

                if (context != null)
                {

                    var item = await ipr.UserPayment(paymentView);
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
