using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noon.MVC.Models;
using System.Diagnostics;

namespace Noon.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "admin, vendor")]
        public IActionResult sales()
        {
            return View("~/Views/DashBoards/dashboard-sales.cshtml");
        }

        [Authorize(Roles = "admin, vendor")]
        public IActionResult finance()
        {
            return View("~/Views/DashBoards/dashboard-finance.cshtml");
        }

        [Authorize(Roles = "admin, vendor")]
        public IActionResult Index()
        {
            return View("~/Views/DashBoards/Index.cshtml");
         //  return View("~/Views/DashBoards/MyLayout.cshtml");

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
