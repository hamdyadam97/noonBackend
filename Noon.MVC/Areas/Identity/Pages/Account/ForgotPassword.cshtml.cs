// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AliExpress.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Noon.MVC.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }




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




        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null )
                {
                    
                    ViewData["Error"] = "User not found";
                    return Page(); 
                }


                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("hamdyadam543@gmail.com"); // Replace with your sender email
                mailMessage.To.Add(new MailAddress(Input.Email));
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
                ViewData["Success"] = "Reset code has been sent to your email. Please check your inbox.";

                return Page();



            }

            // If ModelState is not valid, stay on the same page and display any validation errors
            ViewData["Error"] = "Failed to send reset code. Please try again later.";
            return Page();
        }

    }
}
