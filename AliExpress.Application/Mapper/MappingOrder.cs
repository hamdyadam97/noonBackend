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
            CreateMap<Order, OrderReturnDto>()
           .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
           .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.Name))
           .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
           .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()))
           .ReverseMap();

            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Order,OrderDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Title))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.Images.Select(img =>img.Url))).ReverseMap();
        }
    }
}
