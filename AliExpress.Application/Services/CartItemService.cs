using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.Cart;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepository cartItemRepository,
            IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }
        //public async Task AddCartItemAsync(CartItemDto cartItemDto)
        //{
        //    var mappedCart = _mapper.Map<CartItemDto, CartItem>(cartItemDto);
        //    await _cartItemRepository.AddCartItemAsync(mappedCart);
        //}

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            await _cartItemRepository.DeleteCartItemAsync(cartItemId);
        }

        //public async Task UpdateCartItemAsync(CartItemDto cartItemDto,int cartItemId)
        //{
        //    var mappedCart = _mapper.Map<CartItemDto, CartItem>(cartItemDto);
        //    await _cartItemRepository.UpdateCartItemAsync(mappedCart, cartItemDto.CartItemId);
        //}
    }
}