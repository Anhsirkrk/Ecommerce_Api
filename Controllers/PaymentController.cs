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

                    var item = await ipr.UserPayment3(paymentView);
                    return Ok(item);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("PaymentStatusEmail")]
        public async Task<string> PaymentStatusEmail(int userid, string status, string amount , string email)
        {
            try
            {
                if (context != null)
                {
                    return await ipr.PaymentStatusEmail(userid, status, amount,email);
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
