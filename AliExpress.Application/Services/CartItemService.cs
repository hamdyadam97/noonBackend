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

        public async Task AddOrUpdateCartItemAsync(CartItemDto cartItemDto)
        {
            var cartItem = _mapper.Map<CartItemDto, CartItem>(cartItemDto);
            await _cartItemRepository.AddOrUpdateCartItemAsync(cartItem);
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
           await _cartItemRepository.DeleteCartItemAsync(cartItemId);
        }

        public async Task<List<CartItemDto>> GetCartItemsByCartIdAsync(int cartId)
        {
            var cartItems = await _cartItemRepository.GetCartItemsByCartIdAsync(cartId);
            var mappedCartItem=  _mapper.Map<List<CartItem>, List<CartItemDto>>(cartItems);
            return mappedCartItem;
        }
    }
}
