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
        //Task AddCartItemAsync(CartItemDto cartItemDto);
        //Task UpdateCartItemAsync(CartItemDto cartItemDto , int cartItemId);
        Task DeleteCartItemAsync(int cartItemId);  
    }
}
