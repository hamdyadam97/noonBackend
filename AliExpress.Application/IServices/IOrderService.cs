using AliExpress.Dtos.Order;
using AliExpress.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface IOrderService
    {
        Task<OrderReturnDto> CreateOrderAsync(int cartId, int deliveryMethodId, string userId);
        Task<IEnumerable<OrderReturnDto>> GetAllOrdersAsync();
        Task<OrderReturnDto> GetOrderByUserIdAsync(string userId);
        Task<OrderReturnDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods();
        Task UpdateOrderAsync(int orderId, OrderReturnDto orderReturnDto);
        //Task UpdateOrderByAdminAsync(int orderId, OrderStatusDto orderStatusDto);
        Task DeleteOrderAsync(int orderId);
    }
}
