using Ecommerce_Api.Model;
using Ecommerce_Api.Repository;
using Ecommerce_Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EcommercedemoContext context;
        private readonly IUserRepository iur;
        public UserController(IUserRepository _iur, EcommercedemoContext _context)
        {
            context = _context;
            iur = _iur;
        }
        [HttpPost]
        [Route("CreateUser")]

        public async   Task<IActionResult>  CreateUser(LoginViewModel loginViewModel)
        {
            try
            {
                var item = await iur.CreateUser(loginViewModel);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
