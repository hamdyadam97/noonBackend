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

        public async Task AddCartItemAsync(CartItem cartItem)
        {
         var existedCartItem = await GetCartItemByCartIdAndProductId(cartItem.CartId,cartItem.ProductId);
            if (existedCartItem != null)
            {
                existedCartItem.Quantity += cartItem.Quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                var x = cartItem;
                await _context.CartItems.AddAsync(cartItem);
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

        public async Task<CartItem> GetCartItemByCartIdAndProductId(int cartId, int productId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(ci =>ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FindAsync(cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            var existedCartItem = await GetCartItemByCartIdAndProductId(cartItem.CartId, cartItem.ProductId);
            if (existedCartItem.Quantity != cartItem.Quantity)
            {
                existedCartItem.Quantity = cartItem.Quantity;
                
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
