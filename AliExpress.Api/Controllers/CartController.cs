using AliExpress.Application.IServices;
using AliExpress.Dtos.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Security.Claims;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public CartController(ICartService cartService,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
         
        private bool IsLoggedIn()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
        private string GetUserId()
        {

            //return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier )?.Value;
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }



        //add cart
        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CreateCartDto createCartDto,string userId)
        {
            if (ModelState.IsValid)
            {
                if(userId !=null)
                {
                    await _cartService.CreateCartAsync(createCartDto);
                    return Ok();

                }

                else
                {
                    var sessionCart = _httpContextAccessor.HttpContext.Session;
                    var existingCart = sessionCart.GetString("Cart");
                    if (existingCart != null)
                    {
                        sessionCart.SetString("Cart", JsonSerializer.Serialize(createCartDto));
                    }


                    return Ok();
                }
            }
            return BadRequest();
        }





        //[HttpPost]
        //public async Task<IActionResult> AddCart([FromBody] CreateCartDto createCartDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (IsLoggedIn())
        //        {

        //            await _cartService.CreateCartAsync(createCartDto);
        //            return Ok();
        //        }
        //        else
        //        {
        //            var sessionCart = _httpContextAccessor.HttpContext.Session;
        //            var existingCart = sessionCart.GetString("Cart");
        //           if(existingCart != null)
        //            {
        //                sessionCart.SetString("Cart", JsonSerializer.Serialize(createCartDto));
        //            }


        //            return Ok();
        //        }
        //    }
        //    return BadRequest();
        //}




        [HttpPost("AddCartItem")]
        public async Task<IActionResult> AddCart([FromBody] CartItemDto cartItemDto)
        {
            if (ModelState.IsValid)
            {
                if (IsLoggedIn())
                {

                    var userId = GetUserId();
                    await _cartService.AddOrUpdateCartItemAsync(userId, cartItemDto);
                    return Ok();
                }
                else
                {
                    var sessionCart = _httpContextAccessor.HttpContext.Session;
                    var existingCart = sessionCart.GetString("Cart");
                    List<CartItemDto> cartItems;
                    if (!string.IsNullOrEmpty(existingCart))
                    {
                        cartItems = JsonSerializer.Deserialize<List<CartItemDto>>(existingCart);
                    }
                    else
                    {
                        cartItems = new List<CartItemDto>();
                    }
                    cartItems.Add(cartItemDto);
                    sessionCart.SetString("Cart", JsonSerializer.Serialize(cartItems));
                    return Ok();
                }
            }
            return BadRequest();
        }



        //[HttpPost]
        //public async Task<IActionResult> AddCart([FromBody] CartItemDto cartItemDto)
        //{
        //    var userId = GetUserId();
        //    if (ModelState.IsValid)
        //    {

        //        if (IsLoggedIn())
        //        {


        //            await _cartService.AddOrUpdateCartItemAsync(userId, cartItemDto);
        //            return Ok();

        //        }
        //        else
        //        {
        //            var sessionCart = _httpContextAccessor.HttpContext.Session;
        //            var serializedCart = JsonSerializer.Serialize(cartItemDto);
        //            sessionCart.SetString("Cart", serializedCart);
        //        }
        //        return Ok();
        //    }
        //    return BadRequest();
        //}




        //get cart

        [HttpGet]
    public async Task<IActionResult> GetCart(int cartId)
    {
            var userId = GetUserId();

            if (IsLoggedIn())
        {
                var cart = await _cartService.GetCartByUserIdAsync(userId, cartId);
            return Ok(cart);
        }
        else
        {
            string cartSerializer = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            if (cartSerializer != null)
            {
                var cart = JsonSerializer.Deserialize<CartDto>(cartSerializer);
                return Ok(cart);
            }
            return NotFound();
        }
    }

       


        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            try
            {
                await _cartService.ClearCartAsync(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            try
            {
                await _cartService.RemoveCartItemAsync(cartItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





    }
}
