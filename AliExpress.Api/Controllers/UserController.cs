using AliExpress.Application.IServices;
using AliExpress.Application.Services;
using AliExpress.Dtos.Payment;
using AliExpress.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
           _userService = userService;
        }
        //[HttpGet("{userId}")]
        [HttpGet]
        //public async Task<IActionResult> GetUserById(string userId,string token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjdkNzVmNTNkLTRiNzMtNGE1Yi05NDRlLWVmMTY3NGViZTU2OCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFzZEBnbWFpbC5jb20iLCJleHAiOjE3MTI2MzQ1MjgsImlzcyI6InlvdXJfaXNzdWVyIiwiYXVkIjoieW91cl9pc3N1ZXIifQ.-Luo-3U2YtW_y2cW8Qv3L1xIvUQCHsejNsTp3Uok-o4")
        //{

        //    try
        //    {
        //        var user = await _userService.GetUserByIdAsync(userId);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(userId);
            //return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            // string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjdkNzVmNTNkLTRiNzMtNGE1Yi05NDRlLWVmMTY3NGViZTU2OCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFzZEBnbWFpbC5jb20iLCJleHAiOjE3MTI2MzU2NjEsImlzcyI6InlvdXJfaXNzdWVyIiwiYXVkIjoieW91cl9pc3N1ZXIifQ.rDEBSdd09r9wpIG77fBi_SFnqqWO0v0wBmEMN7cELaw";
            //try
            //{
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        return BadRequest("Token is empty or null.");
            //    }

            //    // Decode the JWT token
            //    var handler = new JwtSecurityTokenHandler();
            //    var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            //    // Extract user ID from the subject claim
            //    var userId = tokenS?.Subject;

            //    if (string.IsNullOrEmpty(userId))
            //    {
            //        // If the user ID is null or empty, return BadRequest
            //        return BadRequest("User ID not found in token.");
            //    }

            //    // Use the extracted user ID to retrieve user information
            //    var user = await _userService.GetUserByIdAsync(userId);
            //    if (user == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(user);
            //}
            //catch (SecurityTokenException ex)
            //{
            //    return Unauthorized(); // Token is invalid
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex.Message}");
            //}
        }



        [HttpPut]
        public async Task<IActionResult> Update(APIUserDTO aPIUserDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Update(aPIUserDTO);
                if (user == null)
                    return BadRequest("Error in Update Try Agin Later ");
                else
                {
                    var updateURL = Url.Link("GetID", new { id = user.Entity.Id });
                    return Ok("User updated successfully.");

                }
            }
            return BadRequest(ModelState);
        }
    }
}
