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
    public class CartRepository : ICartRepository
    {
        private readonly AliExpressContext _context;

        public CartRepository(AliExpressContext context)
        {
            _context = context;
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            _context.SaveChanges();

        }

        public async Task AddOrUpdateCartItem(CartItem cartItem, int cartId)
        {

            var cart = await _context.Carts
             .Include(c => c.CartItems)
             .ThenInclude(ci => ci.Product)
             .FirstOrDefaultAsync(c => c.CartId == cartId);

            var existingProduct = await _context.Products.FindAsync(cartItem.ProductId);
            if (existingProduct == null)
            {

                throw new ArgumentException($"Product with Id {cartItem.ProductId} does not exist");
            }


            cartItem.Product = existingProduct;
            existingProduct.quantity--;
           if(existingProduct.quantity <=0 )
            {
                throw new ArgumentException($"Product does not exist");
            }

                var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);
                if (existingCartItem != null)
                {

                    existingCartItem.Quantity += cartItem.Quantity;
                    _context.CartItems.Update(existingCartItem);
                }
                else
                {

                    cart.CartItems.Add(cartItem);
                }
                cart.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);
           
           

            await _context.SaveChangesAsync();

        }



        //public async Task AddOrUpdateCartItem(CartItem cartItem, int cartId)
        //{
        //    //var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);


        //    if (cartItem == null)
        //    {
        //        throw new ArgumentNullException(nameof(cartItem));
        //    }

        //    try
        //    {
        //        var existingCartItem = await _context.CartItems
        //            .Include(ci => ci.Product)
        //            .FirstOrDefaultAsync(ci => ci.Cart.CartId == cartId && ci.ProductId == cartItem.ProductId);



        //        if (existingCartItem != null)
        //        {

        //            existingCartItem.Quantity += cartItem.Quantity;
        //            _context.CartItems.Update(existingCartItem);
        //        }
        //        else
        //        {

        //            _context.CartItems.Add(cartItem);
        //        }
        //        //var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);
        //        //cart.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

        //        //cart.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * product.Price);

        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine($"{ex.Message}");
        //        throw;
        //    }
        //}


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
            var cart = await _context.Carts
             .Include(c => c.CartItems)
             .ThenInclude(ci => ci.Product)
             .ThenInclude(i=>i.Images)
             .FirstOrDefaultAsync(c => c.AppUserId == userId);

            return cart;

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




        //public async Task AddOrUpdateCartItem(CartItem cartItem, AppUser userId)
        //{
        //    // Find the cart associated with the provided userId
        //    var cart = await _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(ci => ci.Product) // Include the Product navigation property
        //        .FirstOrDefaultAsync(c => c.AppUserId == userId.Id);

        //    if (cart == null)
        //    {
        //        // If the cart doesn't exist, create a new one
        //        var newCart = new Cart { AppUser = userId };
        //        _context.Carts.Add(newCart);
        //        await _context.SaveChangesAsync();

        //        // Retrieve the newly created cart
        //        cart = await _context.Carts
        //            .Include(c => c.CartItems)
        //             .ThenInclude(ci => ci.Product) // Include the Product navigation property
        //            .FirstOrDefaultAsync(c => c.AppUserId == userId.Id);
        //    }

        //    // Check if the provided productId exists in the Products table
        //    var existingProduct = await _context.Products.FindAsync(cartItem.ProductId);
        //    if (existingProduct == null)
        //    {
        //        // If the product doesn't exist, return with an error
        //        throw new ArgumentException($"Product with Id {cartItem.ProductId} does not exist.");
        //    }

        //    // Associate the existing product with the cartItem
        //    cartItem.Product = existingProduct;

        //    // Check if the cart already contains an item with the same productId
        //    var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);
        //    if (existingCartItem != null)
        //    {
        //        // If the cart already contains the product, update the quantity
        //        existingCartItem.Quantity += cartItem.Quantity;
        //        _context.CartItems.Update(existingCartItem);
        //    }
        //    else
        //    {
        //        // If the cart doesn't contain the product, add it as a new cart item
        //        cart.CartItems.Add(cartItem);
        //    }
        //    cart.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

        //    // Save changes to the database
        //    await _context.SaveChangesAsync();
        //}



        //public async Task AddOrUpdateCartItem(CartItem cartItem, AppUser userId)
        //{

        //    var cart = await _context.Carts
        //        .Include(c => c.CartItems)
        //        .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);
        //    if(cart == null)
        //    {

        //        var NewCart=new Cart();
        //        NewCart.AppUser = userId;
        //        _context.Carts.Add(NewCart);

        //        await _context.SaveChangesAsync();
        //        cart = await _context.Carts
        //        .Include(c => c.CartItems)
        //        .FirstOrDefaultAsync(c => true);
        //        cart.CartItems.Add(cartItem);
        //    }
        //    else
        //    {
        //        var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);
        //        if (existingCartItem != null)
        //        {

        //            existingCartItem.Quantity += cartItem.Quantity;
        //            existingCartItem.Product.Price = cartItem.Product.Price;

        //            _context.CartItems.Update(existingCartItem);
        //        }
        //        else
        //        {

        //            cart.CartItems.Add(cartItem);
        //        }

        //    }



        //    cart.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

        //    await _context.SaveChangesAsync();
        //}






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