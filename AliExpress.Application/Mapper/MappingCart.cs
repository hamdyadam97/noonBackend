using AliExpress.Dtos.Cart;
using AliExpress.Dtos.User;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Mapper
{
    public class MappingCart:Profile
    {
        public MappingCart()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Cart,CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>()
                .ForMember(d => d.ProductTitle, o => o.MapFrom(s => s.Product.Title))
                .ForMember(d => d.ProductPrice, o => o.MapFrom(s => s.Product.Price)).ReverseMap();
        }
    }
}
