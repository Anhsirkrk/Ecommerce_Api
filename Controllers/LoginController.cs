using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Model;
using System.Reflection;
using Ecommerce_Api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce_Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly EcommerceDailyPickContext context;
        private readonly IloginRepository _ipr;
        public LoginController(IloginRepository ipr, EcommerceDailyPickContext _context)
        {
            _ipr = ipr;
            context = _context;
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
                throw ex;
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
                            return Ok(user);

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
                throw ex;
            }

        }




    }
}
