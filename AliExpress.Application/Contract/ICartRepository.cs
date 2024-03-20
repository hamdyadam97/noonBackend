using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface ICartRepository
    {
        Task AddCartToUserAsync(Cart cart);
        Task<Cart> GetCartByUserIdAsync(string userId, int cartId);
        Task AddOrUpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int cartId);
        Task DeleteCartItemAsync(int cartItemId);
    }
}
