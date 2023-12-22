using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Model;
using System.Reflection;
using Ecommerce_Api.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IloginRepository _ipr;
        private readonly ILogger _logger;

        private readonly DatabaseLogger _databaseLogger;

        public LoginController(IloginRepository ipr, EcommerceDailyPickContext _context, ILogger logger, DatabaseLogger databaselogger)
        {
            _ipr = ipr;
            context = _context;
            _logger = _logger;
            _databaseLogger = databaselogger;
            context.Database.SetCommandTimeout(120);

        }

        //[HttpGet("{mobileNumber}")]
        //[Route("GetUserByMobileNumber")]
        
        [HttpPost]
        [Route("GetUserByMobileNumber")]
        public async Task<IActionResult> GetUserByMobileNumber([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (context != null)
                {
                    if (loginViewModel != null)
                    {
                        var user = await _ipr.GetUserByMobileNumber(loginViewModel);
                        if (user != null && user.UserFound == true)
                        {
                            string token = JwtToken.GenerateToken(user);
                            //var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                            //_logger.LogInformation($"Roles: {string.Join(", ", roles)}");

                            return Ok(new { Token = token, User = user });


                        }
                        else
                        {
                            return Ok(user);
                        }

                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _databaseLogger.SetUserId(!string.IsNullOrEmpty(loginViewModel.Mobile)
    ? loginViewModel.Mobile
    : !string.IsNullOrEmpty(loginViewModel.Email)
        ? loginViewModel.Email
        : "UnknownUser"); // Set the userId in the DatabaseLogger
                _logger.LogError(ex, "An error occurred while logging in through mobile");
                return BadRequest("an error occured while logging in ");  
            }

        }

        [HttpPost]
        [Route("GetUserByEmails")]
        public async Task<IActionResult> GetUserByEmails([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (context != null)
                {
                    if (loginViewModel != null)
                    {
                        var user = await _ipr.GetUserByEmail(loginViewModel);
                        if (user != null && user.UserFound == true)
                        {
                            string token = JwtToken.GenerateToken(user);
                            return Ok(new { Token = token, User = user });
                          

                        }
                        else
                        {
                            return Ok(user);
                        }

                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _databaseLogger.SetUserId(!string.IsNullOrEmpty(loginViewModel.Email)
     ? loginViewModel.Mobile
     : !string.IsNullOrEmpty(loginViewModel.Email)
         ? loginViewModel.Email
         : "UnknownUser"); // Set the userId in the DatabaseLogger
                _logger.LogError(ex, "An error occurred while logging in through Email");
                return BadRequest("an error occured while logging in ");
            }

        }




    }
}
