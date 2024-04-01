using AliExpress.Application.Contract;
using AliExpress.Application.IServices;
using AliExpress.Context;
using AliExpress.Dtos.Payment;
using AliExpress.Dtos.User;
using AliExpress.Dtos.ViewResult;
using AliExpress.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly AliExpressContext _context;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<AppUser> userManager, AliExpressContext context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
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

        public async Task<ResultView<APIUserDTO>> Update(APIUserDTO aPIUserDTO)
        {
            var existinguser = await _userManager.FindByIdAsync(aPIUserDTO.Id);
            if (existinguser == null)
            {
                return new ResultView<APIUserDTO> { IsSuccess = false, Message = "User not found" };
            }
            else
            {
            
            var u = _mapper.Map<APIUserDTO, AppUser>(aPIUserDTO);

            // Detach previously tracked entity, if any
            _context.Entry(u).State = EntityState.Detached;

            await _userManager.UpdateAsync(u);
            return new ResultView<APIUserDTO> { Entity = aPIUserDTO, IsSuccess = true, Message = "User Updated successfully" };

             }

        }










    }
    }

