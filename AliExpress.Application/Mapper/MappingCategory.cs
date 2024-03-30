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
           
           .ForMember(d => d.Specifications, o => o.MapFrom(s => s.Specification.Select(spec => new Specification { Name = spec })))


            .ReverseMap();

        }
    }




}
