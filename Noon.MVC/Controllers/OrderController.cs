using AliExpress.Application.IServices;
using AliExpress.Dtos.Order;
using AliExpress.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Noon.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var orders =await _orderService.GetAllOrdersAsync();
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order =await _orderService.GetOrderByIdAsync(id);
            if(order.Id == id)
            {
                return View(order);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderStatusDto orderStatusDto)
        { if(id != orderStatusDto.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    var order = await _orderService.GetOrderByIdAsync(orderStatusDto.Id);
                    if(id != orderStatusDto.Id)
                    {
                        return NotFound();
                    }
                    await _orderService.UpdateOrderByAdminAsync(id, orderStatusDto);
                    return RedirectToAction(nameof(Index));
                    
                }
                catch (Exception)
                {

                    return RedirectToAction(nameof(Edit));
                }
            }
            return View(orderStatusDto);
        }

    }
}
