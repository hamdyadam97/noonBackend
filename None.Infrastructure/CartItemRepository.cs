using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;

namespace None.Infrastructure
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AliExpressContext _context;

        public CartItemRepository(AliExpressContext context)
        {
            _context = context;
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            var existedCartItem = await GetCartItemByCartIdAndProductId(cartItem.CartId, cartItem.ProductId);
            if (existedCartItem != null)
            {
                existedCartItem.Quantity += cartItem.Quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                var existingProduct = await _context.Products.FindAsync(cartItem.ProductId);
                if (existingProduct != null)
                {
                    cartItem.Product = existingProduct;
                    await _context.CartItems.AddAsync(cartItem);
                    await _context.SaveChangesAsync();
                }

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
            return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems.FindAsync(cartItemId);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem, int cartItemId)
        {
            var oldCartItem = _context.CartItems.FirstOrDefaultAsync(item => item.CartItemId == cartItemId);
            if (oldCartItem != null)
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
}