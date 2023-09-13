using Ecommerce_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Ecommerce_Api.Model;

namespace Ecommerce_Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly EcommercedemoContext context;
        private readonly ILoginRepository _ipr;
        public LoginController(ILoginRepository ipr,EcommercedemoContext _context )
        {
            _ipr = ipr;
            context = _context;
        }

        //[HttpGet("{mobileNumber}")]
        //[Route("GetUserByMobileNumber")]
        [HttpGet]
        [Route("GetUserByMobileNumber")]
        public async Task<IActionResult> GetUserByMobileNumber(LoginViewModel loginViewModel)
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
                            return Ok(user);
                        }
                        else
                        {
                            return BadRequest("User Not Found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (IActionResult)loginViewModel;
        }
          
      

       
    }
}
