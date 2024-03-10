using AliExpress.Dtos.Product;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Mapper
{
    public class MappingProduct :Profile
    {
        public MappingProduct()
        {
            CreateMap<Product,ProductDetailsDto>()
                .ForMember(d =>d.Gender,o => o.MapFrom(s =>s.Gender.ToString()))
                .ForMember(d => d.Images,o => o.MapFrom(s => s.Images.Select(img =>img.Url))).ReverseMap();

            CreateMap<Product, ProductViewDto>()
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Images.Select(img => img.Url))).ReverseMap();

            CreateMap<CreateUpdateDeleteProductDto, Product>()
                .ForMember(d => d.Gender, o => o.MapFrom(s => Enum.Parse<Gender>(s.Gender)))
                .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(img => new Images { Url = img }))).ReverseMap();
        }
    }
}
