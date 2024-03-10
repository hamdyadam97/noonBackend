using AliExpress.Dtos.Category;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Mapper
{
    public class MappingCategory :Profile
    {
        public MappingCategory()
        {
           CreateMap<Category, CategoryDto>()
           .ForMember(d =>d.Subcategories,o => o.MapFrom(s =>s.Subcategories.Select(sc =>sc.Name))).ReverseMap();
        }
    }
}
