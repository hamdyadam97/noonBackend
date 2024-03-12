using AliExpress.Dtos.Subcategory;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Mapper
{
    public class MappingImages :Profile
    {
        public MappingImages()
        {
            CreateMap<Subcategory, SubCategoryDto>()
             .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
             .ForMember(d => d.ProductCategories, o => o.MapFrom(s => s.ProductCategories.Select(pc => pc.Product.Title))).ReverseMap();
        }
    }
}
