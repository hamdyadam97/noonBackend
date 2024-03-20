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
    public class CartItemRepository:ICartItemRepository
    {
        private readonly AliExpressContext _context;

        public CartItemRepository(AliExpressContext context)
        {
            _context = context;
        }
        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
        }

        public async Task AddOrUpdateCartItemAsync(CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems
                                            .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId && c.ProductId == cartItem.ProductId);

            if (existingCartItem == null)
            {
                
               _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
            }
            else
            {

                existingCartItem.Quantity += cartItem.Quantity;
                _context.CartItems.Update(existingCartItem);
                await _context.SaveChangesAsync();
            }

           
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
    }
}
