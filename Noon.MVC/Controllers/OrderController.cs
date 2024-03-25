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
        public async Task<IActionResult> Edit(int id, OrderReturnDto orderReturnDto)
        { if(id != orderReturnDto.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    var order = await _orderService.GetOrderByIdAsync(orderReturnDto.Id);
                    if(id != orderReturnDto.Id)
                    {
                        return NotFound();
                    }
                    //await _orderService.UpdateOrderByAdminAsync(id, orderStatusDto);
                    await _orderService.UpdateOrderAsync(id, orderReturnDto);
                    return RedirectToAction(nameof(Index));
                    
                }
                catch (Exception)
                {

                    return RedirectToAction(nameof(Edit));
                }
            }
            return View(orderReturnDto);
        }

    }
}
