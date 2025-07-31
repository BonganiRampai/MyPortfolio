using Microsoft.AspNetCore.Mvc;
using Portfolio.Data;
using Portfolio.Models.ViewModels;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers
{
   
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                _emailService.SendEmail(model);
                ViewBag.Message = "Message sent successfully!";
                return View(); // reload the same view with message
            }

            return View(model); // show errors if invalid
        }
    }
}


