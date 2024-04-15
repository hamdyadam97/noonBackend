using AliExpress.Application.IServices;
using AliExpress.Dtos.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AliExpress.Models;


namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;
        public CartController(ICartService cartService,
           IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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




        private async Task<AppUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }

        //add cart

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartItemDto cartItemDto)
        {

            if (ModelState.IsValid)
            {

                if (IsLoggedIn())
                {

                    await _cartService.AddOrUpdateCartDtoAsync(cartItemDto, cartItemDto.CartId);

                }
                else
                {
                    var sessionCart = _httpContextAccessor.HttpContext.Session;
                    var serializedCart = JsonSerializer.Serialize(cartItemDto);
                    sessionCart.SetString("Cart", serializedCart);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("createCart")]
        public async Task<IActionResult> createUserCart(CreateCartDto createCartDto)
        {
            if (ModelState.IsValid)
            {
                if (IsLoggedIn())
                {

                    await _cartService.createUserCart(createCartDto);

                }
                else
                {
                    var sessionCart = _httpContextAccessor.HttpContext.Session;
                    var serializedCart = JsonSerializer.Serialize(createCartDto);
                    sessionCart.SetString("Cart", serializedCart);
                }
                return Ok();
            }
            return BadRequest();
        }

        //get cart

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            if (IsLoggedIn())
            {
                var cart = await _cartService.GetCartDtoByUserIdAsync(userId);

                return Ok(cart);
            }
            return NotFound();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCart()
        //{
        //    var userId = GetUserId();
        //    if (IsLoggedIn())
        //    {
        //        var cart = await _cartService.GetCartDtoByUserIdAsync(userId);
        //        return Ok(cart);
        //    }
        //    else
        //    {
        //        string cartSerializer = _httpContextAccessor.HttpContext.Session.GetString("Cart");
        //        if (cartSerializer != null)
        //        {
        //            var cart = JsonSerializer.Deserialize<CartDto>(cartSerializer);
        //            return Ok(cart);
        //        }
        //        return NotFound();
        //    }
        //}



        [HttpGet("GetCartByUserId")]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            var cartDto = await _cartService.GetCartDtoByUserIdAsync(userId);
            if (cartDto == null)
            {
                return NotFound();
            }
            return Ok(cartDto);
        }


        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            await _cartService.DeleteCartDtoAsync(cartId);
            return Ok();
        }

        //[HttpPut("{cartId}")]
        //public async Task<IActionResult> UpdateCart([FromBody] CartDto cartDto, int cartId)
        //{
        //    var userId = GetUserId();
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return NotFound();
        //    }
        //    await _cartService.UpdateCartDtoAsync(cartDto, cartId);
        //    return Ok();
        //}





    }
}