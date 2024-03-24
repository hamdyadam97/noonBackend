using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.User;
using AliExpress.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliExpress.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
       public async Task<AppUserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var mappedUser= _mapper.Map<AppUser,AppUserDto>(user);
            return mappedUser;
        }
    }
    }

