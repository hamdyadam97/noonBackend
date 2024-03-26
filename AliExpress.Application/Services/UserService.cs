using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Dtos.User;
using AliExpress.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        
        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
       public async Task<AppUserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var mappedUser= _mapper.Map<AppUser,AppUserDto>(user);
            return mappedUser;
        }

        public List<UserDTO> GetUsers()
        {
            var allUsers = _userManager.Users.ToList();
            var userDTOs = _mapper.Map<List<AppUser>, List<UserDTO>>(allUsers);
            return userDTOs;
        }
    }
    }

