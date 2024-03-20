using AliExpress.Application.IServices;
using AliExpress.Dtos.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCartItem([FromBody] CartItemDto cartItemDto)
        {
            try
            {
                await _cartItemService.AddOrUpdateCartItemAsync(cartItemDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            try
            {
                await _cartItemService.DeleteCartItemAsync(cartItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("cart/{cartId}")]
        public async Task<IActionResult> GetCartItemsByCartId(int cartId)
        {
            try
            {
                var cartItems = await _cartItemService.GetCartItemsByCartIdAsync(cartId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
