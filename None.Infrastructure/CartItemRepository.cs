using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class CartItemRepository:ICartItemRepository
    {
        private readonly AliExpressContext _context;

        public CartItemRepository(AliExpressContext context)
        {
            _context = context;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FindAsync(cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            if (cartItem != null && cartItem.CartItemId != null)
            {
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
