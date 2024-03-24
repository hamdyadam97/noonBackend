using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using AliExpress.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AliExpressContext _context;

        public OrderRepository(AliExpressContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(int cartId, int deleveryMethodId, AppUser appUser)
        {
            //var cart = await _context.Carts.FirstOrDefaultAsync(c =>c.CartId == cartId);
            var cart = await _context.Carts.Include(c=>c.CartItems).FirstOrDefaultAsync(c => c.CartId == cartId);
            var orderItems = new List<OrderItem>();
            if (cart?.CartItems?.Count > 0)
            {
                foreach (var item in cart.CartItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    var orderItem=new OrderItem(product,item.Quantity, item.Product.Price);
                    orderItems.Add(orderItem);
                    await _context.SaveChangesAsync();
                }
               
            }

            //calc subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var deliveryMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(d => d.Id == deleveryMethodId);
            //create order
            var order = new Order(deliveryMethod, appUser,orderItems, subtotal);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders=await _context.Orders.Include(o => o.DeliveryMethod).ToListAsync();
            return orders;
        }

        public async Task<AppUser> GetAppUserAsync()
        {
            var appUser=await _context.Users.FirstOrDefaultAsync();
            return appUser;
        }

        public async Task<Order> GetByUserIdAsync(string userId)
        {
            var order = await _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.DeliveryMethod)
            .Where(o => o.AppUserId == userId)
            .FirstOrDefaultAsync();
            return order;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            var delveryMethods = await _context.DeliveryMethods.ToListAsync();
            return delveryMethods;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            //var order = await _context.Orders.FindAsync(orderId);
            //return order;
            var order = await _context.Orders
                              .Include(o => o.DeliveryMethod)
                              .Include(o => o.OrderItems)
                              .FirstOrDefaultAsync(o => o.Id == orderId);
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }


    }
}
