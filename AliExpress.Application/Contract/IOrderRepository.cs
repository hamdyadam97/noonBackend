using AliExpress.Models;
using AliExpress.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(int cartId, int deleveryMethodId,AppUser appUser);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> GetByUserIdAsync(string userId);
        Task<AppUser> GetAppUserAsync();
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods();
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
