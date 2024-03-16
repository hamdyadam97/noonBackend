using AliExpress.Dtos.User;
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

        private void SendConfirmationEmail(string email)
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
        }


        public AccountController(
      UserManager<AppUser> userManager,
      SignInManager<AppUser> signInManager,
       IConfiguration configuration)
        {
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
                    SendConfirmationEmail(model.Email);
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
    }
}
