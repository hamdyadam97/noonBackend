using AliExpress.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IOrderService _orderService;
        public ReportController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task< IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
           
        }
    }
}
