using AliExpress.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface ICartService
    {

        Task<CartDto> GetCartByUserIdAsync(string userId, int cartId);
        Task AddOrUpdateCartItemAsync(string userId, CartItemDto cartItemDto);
        Task CreateCartAsync(CreateCartDto createCartDto);
        Task RemoveCartItemAsync(int cartItemId);
        Task ClearCartAsync(string userId);
    }
}
