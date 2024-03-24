using AliExpress.Dtos.Cart;
using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface ICartService
    {
        Task<CartDto> GetCartDtoByUserIdAsync(string userId);
        Task createUserCart(CartDto cartDto);
        Task AddOrUpdateCartDtoAsync(CartItemDto cartItemDto, AppUser userId);
        //Task UpdateCartDtoAsync(int cartId);
        Task DeleteCartDtoAsync(int cartId);

    }
}