using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GAM.Models;

namespace GAM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View("HomeLogged");
            return View();
        }

        public IActionResult IndexRegistered()
        {
            return View();
        }

        public IActionResult IndexAnsweredAS()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
