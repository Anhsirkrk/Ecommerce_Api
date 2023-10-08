using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    public class EmptyController : Controller
    {
        public IActionResult Index1()
        {
            return View();
        }
    }
}
