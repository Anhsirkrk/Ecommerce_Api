using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    public class EmptyController2 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
