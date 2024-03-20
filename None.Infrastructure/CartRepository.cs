using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class CartRepository:ICartRepository
    {
        private readonly AliExpressContext _context;
       
        public CartRepository(AliExpressContext context)
        {
            _context = context;
          
        }



        public async Task<Cart> GetCartByUserIdAsync(string userId,int cartId)
        {
            //return await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId && c.CartId == cartId);
            return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CartId == cartId && c.UserId == userId);
        }
        public async Task AddOrUpdateCartAsync(Cart cart)
        {
            if (cart.CartId == 0)
            {
                cart.TotalAmount = CalculateTotalAmount(cart.Items);
                _context.Carts.Add(cart);
            }
            else
            {
                cart.TotalAmount = CalculateTotalAmount(cart.Items);
                _context.Carts.Update(cart);
            }

            
          

            await _context.SaveChangesAsync();
        }
        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        private decimal CalculateTotalAmount(List<CartItem> items)
        {
            
            decimal totalAmount = 0;

            foreach (var item in items)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                totalAmount += item.Quantity * product.Price;
            }

            return totalAmount;
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
           var cartItem = await _context.CartItems.FindAsync(cartItemId);
             _context.CartItems.Remove(cartItem);
          await _context.SaveChangesAsync();
        }

        public async Task AddCartToUserAsync(Cart cart)
        {
             await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
    }
}
