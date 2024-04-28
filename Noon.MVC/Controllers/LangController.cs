using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Noon.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    [Authorize]
    public class LangController : Controller
    {
        
        
            public IActionResult SetLanguage(string lang)
            {
                // Store selected language in session
                HttpContext.Session.SetString("Culture", lang);

                // Redirect back to the previous page
                return Redirect(Request.Headers["Referer"].ToString());
            }
            public IActionResult Arabic()
            {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ar-EG")), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            HttpContext.Session.SetString("Culture", "ar-EG");
            
                // Set the current culture for the current thread
                CultureInfo.CurrentCulture = new CultureInfo("ar-EG");
                CultureInfo.CurrentUICulture = new CultureInfo("ar-EG");

                string referringUrl = Request.Headers["Referer"].ToString();
                return Redirect(referringUrl);

            /* HttpContext.Session.SetString("lang", "Ar-EG");
                 string referringUrl = Request.Headers["Referer"].ToString();
                 return Redirect(referringUrl);*/
            }
        public IActionResult English()
            {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, 
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US")), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            HttpContext.Session.SetString("Culture", "en-US");

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
