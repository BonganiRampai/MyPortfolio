using Microsoft.AspNetCore.Mvc;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // /Views/Home/Index.cshtml
        }

        public IActionResult About()
        {
            return View(); // /Views/Home/About.cshtml
        }

        public IActionResult Skills()
        {
            return View(); // /Views/Home/Skills.cshtml
        }
        

    }
}
