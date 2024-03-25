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
    public class MappingUser: Profile
    {
        public MappingUser() 
        {
            CreateMap<UserDTO,AppUser>().ReverseMap();
        }
    }
}
