using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AliExpress.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        //public IActionResult login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> login(string username, string password)
        //{
        //    return View();
        //}




    }
}
