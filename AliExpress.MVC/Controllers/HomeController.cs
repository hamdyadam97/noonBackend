using AliExpress.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AliExpress.MVC.Controllers
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
            //return View();

        }


        public IActionResult RedirectToLogin()
        {
            // Redirect to the login action in the Identity/Account controller
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }



        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            // Generate the URL for the login action in the Identity/Account controller
            var loginUrl = Url.Action("Login", "Account", new { area = "Identity" });

            // Redirect to the login page
            return Redirect(loginUrl);
        }




        //public IActionResult logout2()
        //{
        //    return View("~/Areas/Identity/Pages/Account/Logout.cshtml");
        //}





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
