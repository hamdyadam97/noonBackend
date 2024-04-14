using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
namespace Noon.MVC.Controllers
{
    public class LangController : Controller
    {
        
        
            public IActionResult SetLanguage(string lang)
            {
                // Store selected language in session
                HttpContext.Session.SetString("culture", lang);

                // Redirect back to the previous page
                return Redirect(Request.Headers["Referer"].ToString());
            }
            public IActionResult Arabic()
            {
                HttpContext.Session.SetString("culture", "ar-EG");

                // Set the current culture for the current thread
                CultureInfo.CurrentCulture = new CultureInfo("ar-EG");
                CultureInfo.CurrentUICulture = new CultureInfo("ar-EG");

                string referringUrl = Request.Headers["Referer"].ToString();
                return Redirect(referringUrl+ "?culture=ar-Eg");

                /* HttpContext.Session.SetString("lang", "Ar-EG");
                     string referringUrl = Request.Headers["Referer"].ToString();
                     return Redirect(referringUrl);*/
            }


            public IActionResult English()
            {
                HttpContext.Session.SetString("culture", "en-US");

                // Set the current culture for the current thread
                CultureInfo.CurrentCulture = new CultureInfo("en-US");
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");

                string referringUrl = Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);
                /* HttpContext.Session.SetString("lang", "EN-US");
                 string referringUrl = Request.Headers["Referer"].ToString();
                 return Redirect(referringUrl);*/
            }

        }
    
}
