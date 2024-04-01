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
        public async Task<APIUserDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var mappedUser= _mapper.Map<AppUser,APIUserDTO>(user);
            return mappedUser;
        }

        public List<APIUserDTO> GetUsers()
        {
            var allUsers = _userManager.Users.ToList();
            var userDTOs = _mapper.Map<List<AppUser>, List<APIUserDTO>>(allUsers);
            return userDTOs;
        }

        //public async Task<ResultView<APIUserDTO>> Update(APIUserDTO aPIUserDTO)
        //{
        //    var existingUser = await _userManager.FindByIdAsync(aPIUserDTO.Id);
        //    if (existingUser == null)
        //    {
        //        return new ResultView<APIUserDTO> { IsSuccess = false, Message = "User not found" };
        //    }
        //    else
        //    {
        //        // Update properties of existing user
        //        existingUser.FName = aPIUserDTO.FName;
        //        existingUser.LName = aPIUserDTO.LName;
        //        // Update other properties as needed

        //        // Save changes
        //        var result = await _userManager.UpdateAsync(existingUser);

        //        if (result.Succeeded)
        //        {
        //            return new ResultView<APIUserDTO> { Entity = aPIUserDTO, IsSuccess = true, Message = "User Updated successfully" };
        //        }
        //        else
        //        {
        //            // Handle update failure
        //            return new ResultView<APIUserDTO> { IsSuccess = false, Message = "Failed to update user" };
        //        }
        //    }
        //}
        public async Task<ResultView<APIUserDTO>> Update(APIUserDTO aPIUserDTO)
        {
            var existingUser = await _userManager.FindByIdAsync(aPIUserDTO.Id);
            if (existingUser == null)
            {
                return new ResultView<APIUserDTO> { IsSuccess = false, Message = "User not found" };
            }
            else
            {
                // Map DTO properties to existing user entity
                _mapper.Map(aPIUserDTO, existingUser);

                // Update user
                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    return new ResultView<APIUserDTO> { Entity = aPIUserDTO, IsSuccess = true, Message = "User Updated successfully" };
                }
                else
                {
                    // Handle update failure
                    return new ResultView<APIUserDTO> { IsSuccess = false, Message = "Failed to update user" };
                }
            }
        }











    }
}

