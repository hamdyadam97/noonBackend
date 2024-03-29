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

        //public async Task AddOrUpdateCartDtoAsync(CartItemDto cartItemDto, AppUser userId)
        //{
        //    var mappedCart = _mapper.Map<CartItemDto, CartItem>(cartItemDto);
        //    await _cartRepository.AddOrUpdateCartItem(mappedCart, userId);
        //}
        public async Task AddOrUpdateCartDtoAsync(CartItemDto cartItemDto, int cartId)
        {
            var mappedCart = _mapper.Map<CartItemDto, CartItem>(cartItemDto);
            await _cartRepository.AddOrUpdateCartItem(mappedCart, cartId);
        }


        public async Task createUserCart(CreateCartDto createCartDto )
        {
            var mappedCart = _mapper.Map<CreateCartDto, Cart>(createCartDto);
            await _cartRepository.AddCartAsync(mappedCart);
        }

        public async Task DeleteCartDtoAsync(int cartId)
        {
            await _cartRepository.DeleteCartAsync(cartId);
        }

        public async Task<CartDto> GetCartDtoByIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            var cartdtoMapped = _mapper.Map<Cart, CartDto>(cart);
            return cartdtoMapped;
        }

        public async Task<CartDto> GetCartDtoByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);
            var cartdtoMapped = _mapper.Map<Cart, CartDto>(cart);
            return cartdtoMapped;
        }






    }
}