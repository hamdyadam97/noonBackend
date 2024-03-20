using AliExpress.Dtos.Cart;
using AliExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.IServices
{
    public interface ICartItemService
    {
        Task<List<CartItemDto>> GetCartItemsByCartIdAsync(int cartId);
        Task AddOrUpdateCartItemAsync(CartItemDto cartItemDto);
        Task DeleteCartItemAsync(int cartItemId);
    }
}
