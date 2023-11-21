using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//19-10-2023
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Voice.V1.DialingPermissions;
using Microsoft.AspNetCore.Mvc;
using WhatsAppApi;
using System.Net.Mail;
using System.Net;

namespace Ecommerce_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IUserRepository iur;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository _iur, EcommerceDailyPickContext _context, IConfiguration configuration)
        {
            context = _context;
            iur = _iur;
            _configuration = configuration;
            context.Database.SetCommandTimeout(120);
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
                //var accountSid = _configuration["Twilio:AccountSid"];
                //var authToken = _configuration["Twilio:AuthToken"];
                //TwilioClient.Init(accountSid, authToken);

                //var recipientPhoneNumber = "whatsapp:" + item.Mobile; // Make sure item.Mobile is in the correct format

                //var fromPhoneNumber = new PhoneNumber(_configuration["Twilio:PhoneNumber"]);

                //var toPhoneNumber = new PhoneNumber(recipientPhoneNumber);

                //var messageOptions = new CreateMessageOptions(toPhoneNumber)
                //{
                //    From = fromPhoneNumber,
                //    Body = "Hello, your registration on daily pic is successful"
                //};
                //var message = MessageResource.Create(messageOptions);

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

        [HttpPost]
        [Route("UpdateAdressDetails")]
        public async Task<ActionResult<UserAdressViewModel>> UpdateAdressDetails(UserAdressViewModel userAdressViewModel)
        {
            try
            {

                if (userAdressViewModel.AddressId != 0)
                {

                    var item = await iur.UpdateAdressDetails(userAdressViewModel);
                    return Ok(item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //[Route("UpdateAdressDetails")]
        //public async Task<ActionResult<UserAdressViewModel>> UpdateAdressDetails(UserAdressViewModel userAdressViewModel)
        //{
        //    try
        //    {

        //        if (userAdressViewModel.AddressId != 0)
        //        {

        //            var item = await iur.UpdateAdressDetails(userAdressViewModel);
        //            return Ok(item);
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}





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

        //private void SendEmail(string email)
        //{
        //    string senderEmail ="ramakrishna.hds.15@gmail.com";
        //    string senderPassword = "xvdf bpib kdpx fyru";


        //    string toEmail = email;
        //    string subject = "Welcome to Our DailyPick Website!";
        //    string body = "Thank you for registering on our website. We are excited to have you on board!";


        //    MailMessage mail = new MailMessage(senderEmail, toEmail);
        //    mail.Subject = subject;
        //    mail.Body = body;
        //    mail.IsBodyHtml = true;

        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential(senderEmail, senderPassword),
        //        EnableSsl = true,
        //    };

        //    try
        //    {
        //        smtp.Send(mail);
        //    }
        //    catch (SmtpException ex)
        //    {
        //        // Handle SMTP exceptions (e.g., invalid recipient, server not reachable, authentication issues, etc.)
        //        // Log the error and handle it appropriately.
        //        Console.WriteLine("SMTP Exception: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        // Log the error and handle it appropriately.
        //        Console.WriteLine("Exception: " + ex.Message);
        //    }
        //    finally
        //    {
        //        mail.Dispose();
        //        smtp.Dispose();
        //    }
        //}
    }


}

