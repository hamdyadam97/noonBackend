using AliExpress.Dtos.User;
using AliExpress.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using AliExpress.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AliExpress.Application.IServices;

namespace AliExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        private string GenerateVerificationCode()
        {
            const int codeLength = 6;
            StringBuilder code = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code.Append(random.Next(0, 9)); // Add a random digit
            }
            return code.ToString();
        }

        private string SendConfirmationEmail(string email)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("hamdyadam543@gmail.com"); // Replace with your sender email
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Verify Your noon Account";
            string verificationCode = GenerateVerificationCode();
            mailMessage.Body = string.Format("Your verification code is: {0}", verificationCode);
            mailMessage.IsBodyHtml = false; // Set to true for HTML formatting (sanitize user input)

            var smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com"; // Replace with your SMTP server address
            smtpClient.Port = 587; // Replace with your SMTP port (may vary)
            smtpClient.EnableSsl = true; // Use SSL for secure communication
            smtpClient.Credentials = new NetworkCredential("hamdyadam543@gmail.com", "feri kwvj tsim jpst"); // Replace with your SMTP credentials

            smtpClient.Send(mailMessage);
            return verificationCode;
        }


        public AccountController(
      UserManager<AppUser> userManager,
      SignInManager<AppUser> signInManager,
      IUserService userService,
       IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
       
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    return BadRequest("User already exists.");
                }

                var newUser = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(newUser);
                    //SendConfirmationEmail(model.Email);
                    newUser.Code = SendConfirmationEmail(model.Email);
                    await _userManager.UpdateAsync(newUser);
                    return Ok(new { Token = token, User  = newUser });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { Token = token, User = user });
                }
                else
                {
                    return Unauthorized("Invalid email or password.");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("RestPassword")]
        public async Task<IActionResult> RestPassword(RestPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    
                    user.Code = SendConfirmationEmail(model.Email);
                   await _userManager.UpdateAsync(user);
                    return Ok(new { messge = "check your mail"});
                }
                else
                {
                    return Unauthorized("mail not found");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("RestChangePassword")]
        public async Task<IActionResult> RestChangePassword(RestChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && user.Code == model.Code)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        // Handle password change failure
                        return BadRequest(new { message = "Failed to change password", errors = result.Errors });
                    }
                }
                else
                {
                    return Unauthorized("Email or verification code is incorrect");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null &&await _userManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok( result );
                    }
                    else
                    {
                        // Handle password change failure
                        return BadRequest(new { message = "Failed to change password", errors = result.Errors });
                    }
                }
                else
                {
                    return Unauthorized("Email or verification code is incorrect");
                }
            }
            return BadRequest(ModelState);
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserById()
        {
            try
            {
                // Retrieve the user ID from the token
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Retrieve only necessary user data
                var userData = await _userManager.FindByIdAsync(userId);

                if (userData == null)
                {
                    return NotFound();
                }

                return Ok(userData);
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
                   
                    return Ok("User updated successfully.");

                }
            }
            return BadRequest(ModelState);
        }



    }
}
