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

        public async Task AddCartAsync(Cart cart)
        {
           if(cart.CartId == null) 
            _context.Carts.Add(cart);
           await _context.SaveChangesAsync();
        }



        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
                _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
            return cart;
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            var cart=await _context.Carts.Include(c => c.CartItems)
                  .ThenInclude(ci => ci.Product)
                  .FirstOrDefaultAsync(c =>c.AppUserId == userId);
            return cart;
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            if (cart.CartId != null)
                _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
