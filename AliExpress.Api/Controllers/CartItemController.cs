﻿using AliExpress.Application.IServices;
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
        public async Task<IActionResult> AddCartItem(CartItemDto cartItemDto)
        {
            if(ModelState.IsValid)
            {
                await _cartItemService.AddCartItemAsync(cartItemDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(CartItemDto cartItemDto)
        {
            if (ModelState.IsValid)
            {
               await _cartItemService.UpdateCartItemAsync(cartItemDto);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            if(cartItemId == null)
            {
                return NotFound();
            }
            await _cartItemService.DeleteCartItemAsync(cartItemId);
            return Ok();
        }




    }
}
