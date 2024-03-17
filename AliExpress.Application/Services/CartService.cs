﻿using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Cart;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json;
namespace AliExpress.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task AddCartDtoAsync(CartDto cartDto)
        {
            var mappedCart=_mapper.Map<CartDto,Cart>(cartDto);
            await _cartRepository.AddCartAsync(mappedCart);
        }

        public async Task DeleteCartDtoAsync(int cartId)
        {
            await _cartRepository.DeleteCartAsync(cartId);
        }

        public async Task<CartDto> GetCartDtoByUserIdAsync(string userId)
        {
            var cart=await _cartRepository.GetCartByUserId(userId);
            var cartdtoMapped = _mapper.Map<Cart, CartDto>(cart);
            return cartdtoMapped;
        }
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public async Task UpdateCartDtoAsync(CartDto cartDto,int cartId)
        {
            var mappedCart = _mapper.Map<CartDto, Cart>(cartDto);
          
            //var appUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //mappedCart.AppUser = appUser
            await _cartRepository.UpdateCartAsync(mappedCart, cartId);
        }

      


    }
}
