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
        Task<CartDto> GetCartDtoByIdAsync(int cartId);
        Task createUserCart(CreateCartDto createCartDto);
        //Task AddOrUpdateCartDtoAsync(CartItemDto cartItemDto, AppUser userId);
        Task AddOrUpdateCartDtoAsync(CartItemDto cartItemDto, int cartId);
        //Task UpdateCartDtoAsync(int cartId);
        Task DeleteCartDtoAsync(int cartId);

    }
}