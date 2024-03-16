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
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int cartId);
    }
}
