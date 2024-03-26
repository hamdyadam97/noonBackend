using AliExpress.Dtos.Category;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AliExpress.Application.Mapper
{
    public class MappingCategory :Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto,Category>()
            //.ForMember(d => d.Subcategories, o => o.MapFrom(s => s.Subcategories.Select(sc => sc.Name)))

           //.ForMember(d => d.Specification, o => o.MapFrom(s => s.Specifications.Select(spec => new Specification { Name = spec.Name })))
           .ForMember(d => d.Specifications, o => o.MapFrom(s => s.Specification.Select(spec => new Specification { Name = spec })))


            .ReverseMap();


            //.ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(img => new Images { Url = img }))).ReverseMap();
        }
    }




}
