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
        Task<CartDto> GetCartDtoByUserIdAsync(string userId);
        Task AddCartDtoAsync(CartDto cartDto);
        Task UpdateCartDtoAsync(CartDto cartDto, int cartId);
        Task DeleteCartDtoAsync(int cartId);

    }
}
