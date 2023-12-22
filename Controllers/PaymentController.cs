using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IPaymentRepository ipr;
        private readonly ILogger logger;
        private readonly DatabaseLogger databaseLogger;
        public PaymentController(IPaymentRepository _ipr, EcommerceDailyPickContext _context,ILogger _logger,DatabaseLogger _databaselogger)
        {
            context = _context;
            ipr = _ipr;
            logger = _logger;
            databaseLogger = _databaselogger;
            context.Database.SetCommandTimeout(120);
        }


        [Authorize(Roles = "1,2")]
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


        [Authorize(Roles = "1,2")]
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
                //var databaseLogger = (DatabaseLogger)logger; // Cast to DatabaseLogger
                databaseLogger.SetUserId(userid.ToString()); // Set the userId in the DatabaseLogger
                logger.LogError(ex, "An error occurred");
                return "an error occured";
            }
           
        }
    }
}
