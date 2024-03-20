using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Contract
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task AddOrUpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);

    }
}
