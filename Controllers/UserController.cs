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
        private readonly IUserInterface iur;
        public UserController(IUserInterface _iur, EcommercedemoContext _context)
        {
            context = _context;
            iur = _iur;
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
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error :{ex.Message}");
            }
        }
      
    }
}
