using AliExpress.Application.IServices;
using AliExpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        public ReportController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task< IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var allUsers = _userManager.Users.ToList(); 
            decimal? revenue = 0;
            foreach (var order in orders)
                revenue += order.Total;

            var avgRevenue = revenue / allUsers.Count();
            ViewBag.avgRevenue = (int)avgRevenue;
            ViewBag.Revenue = (int)revenue;
            ViewBag.Uer=allUsers.Count();
            ViewBag.vistore=allUsers.Count()*9;
            ViewBag.totalOrder=orders.Count();
            return View(orders);
           
        }
    }
}
