using AliExpress.Dtos.Order;
using AliExpress.Models;
using AliExpress.Models.Orders;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Mapper
{
    public class MappingOrder:Profile
    {
        public MappingOrder()
        {
            // CreateMap<Order, OrderReturnDto>()
            //.ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            //.ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.Name))
            //.ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
            //.ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()))
            //.ReverseMap();

          CreateMap<Order, OrderReturnDto>()
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
        .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.Name))
        .ForMember(dest => dest.DeliveryMethodCost, opt => opt.MapFrom(src => src.DeliveryMethod.Cost))
        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
        .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()))
        .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
        .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser))
        .ReverseMap();






            CreateMap<Order, OrderStatusDto>().ReverseMap();


            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Order,OrderDto>().ReverseMap();
          
            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Title))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.Images.Select(img =>img.Url))).ReverseMap();
        }
    }
}
