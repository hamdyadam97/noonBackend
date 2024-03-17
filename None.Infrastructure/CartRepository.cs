using AliExpress.Application.Contract;
using AliExpress.Context;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace None.Infrastructure
{
    public class CartRepository:ICartRepository
    {
        private readonly AliExpressContext _context;

        public CartRepository(AliExpressContext context)
        {
            _context = context;
        }




        //public async Task AddCartAsync(Cart cart,int cartItemId)
        //{
        //    var exitedCart = await _context.Carts.Include(c => c.CartItems).
        //        FirstOrDefaultAsync(c => c.CartId == cartItemId);
        //    if (exitedCart == null)
        //    {
        //      await  _context.Carts.AddAsync(cart);

        //    }
        //    else
        //    {
        //        cart.TotalAmount = exitedCart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

        //    }
        //    await _context.SaveChangesAsync();
        //}

        public async Task AddCartAsync(Cart cart)
        {
            if (cart == null)
            {
                await _context.Carts.AddAsync(cart);
                _context.SaveChanges();
            }

        }

        public async Task AddOrUpdateCartItem(CartItem cartItem, string userId)
        {
           
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);
            if(cart == null)
            {
                var NewCart=new Cart();
                NewCart.AppUser.Id = userId;
                _context.Carts.Add(NewCart);
            }
       
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);
            if (existingCartItem != null)
            {
             
                existingCartItem.Quantity += cartItem.Quantity;
                existingCartItem.Product.Price = cartItem.Product.Price; 

                _context.CartItems.Update(existingCartItem);
            }
            else
            {
              
                cart.CartItems.Add(cartItem);
            }

         
            cart.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.AppUserId == userId);
        }


      



        //public async Task UpdateCartAsync(Cart cart , int cartId)
        //{
        //    var oldCart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
        //    if(oldCart != null)
        //    {
        //        _context.Carts.Update(cart);
        //        await _context.SaveChangesAsync();
        //    }

        //}
    }
}
