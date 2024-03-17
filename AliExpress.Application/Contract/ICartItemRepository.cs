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
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(int cartItemId);
        Task<CartItem> GetCartItemByCartIdAndProductId(int cartId,int productId);

    }
}
