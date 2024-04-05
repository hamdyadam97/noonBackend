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
        

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserById()
        {

            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
                    //var updateURL = Url.Link("GetID", new { id = user.Entity.Id });
                    return Ok("User updated successfully.");

                }
            }
            return BadRequest(ModelState);
        }
    }
}
