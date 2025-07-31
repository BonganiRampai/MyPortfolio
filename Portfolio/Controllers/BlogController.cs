using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
