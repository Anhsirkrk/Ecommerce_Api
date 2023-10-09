using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    public class MyTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
