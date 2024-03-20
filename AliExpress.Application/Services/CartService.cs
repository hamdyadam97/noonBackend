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
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddOrUpdateCartItemAsync(string userId, CartItemDto cartItemDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var cart = user.Cart ?? new Cart();
            cart.UserId = userId;
            cart.Items ??= new List<CartItem>();

           
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == cartItemDto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                var newItem = _mapper.Map<CartItemDto, CartItem>(cartItemDto);
                cart.Items.Add(newItem);
            }

            await _cartRepository.AddOrUpdateCartAsync(cart);
        }

        public async Task ClearCartAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || user.Cart == null)
                return;

            await _cartRepository.DeleteCartAsync(user.Cart.CartId);
        }

        public async Task CreateCartAsync(CreateCartDto createCartDto)
        {
            var mappedCart = _mapper.Map<CreateCartDto, Cart>(createCartDto);
            await _cartRepository.AddCartToUserAsync(mappedCart);
        }

        public async Task<CartDto> GetCartByUserIdAsync(string userId, int cartId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId, cartId);

            var mappedCart= _mapper.Map<Cart,CartDto>(cart);
            return mappedCart;
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _cartRepository.DeleteCartItemAsync(cartItemId);
        }
    }
}
