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
        Task<Cart> GetCartByUserId(string userId);
        Task<Cart> GetCartByIdAsync(int cartId);
        Task AddCartAsync(Cart cart);
        //Task AddCartAsync(Cart cart,int cartItemId);
        //Task UpdateCartAsync(Cart cart ,int cartId);
        //Task UpdateCartAsync( int cartId, int cartItemId);
        Task AddOrUpdateCartItem(CartItem cartItem, AppUser userId);
        Task DeleteCartAsync(int cartId);
    }
}