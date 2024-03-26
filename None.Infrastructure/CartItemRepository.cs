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

       

        public async Task DeleteCartItemAsync(int cartItemId)
        {

            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            //var cart = await _context.Carts
            // .Include(c => c.CartItems)
            // .ThenInclude(ci => ci.Product) 
            // .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);
            //cart.TotalAmount = cart.TotalAmount - cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

      

    }
}