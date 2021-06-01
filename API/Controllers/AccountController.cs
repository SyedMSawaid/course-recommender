using API.Data;
using API.DTOs;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("User Already Exist");

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Email = registerDto.Email,
                FullName = registerDto.FullName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result);

            var roleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!roleResult.Succeeded) return BadRequest(result);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            loginDto.Username = loginDto.Username.ToLower();
            AppUser user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid username or Password");
            
            return Ok(new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            });
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<UserDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            AppUser userToChange = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == changePasswordDto.UserId);
            var succeeded = await _userManager.ChangePasswordAsync(userToChange, changePasswordDto.OldPassword,
                changePasswordDto.NewPassword);

            if (succeeded.Succeeded)
            {
                AppUser user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == changePasswordDto.UserId);
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await _tokenService.CreateToken(user)
                };
            }

            return Unauthorized(succeeded.Errors);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null) return NotFound("No account is associated with this email.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"http://localhost:4200/resetpassword?email={forgetPasswordDto.Email}&token={validToken}";
            
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("e9eb97c23bc1f7", "8886e8969d3dd5"),
                EnableSsl = true
            };
            client.Send("from@example.com", forgetPasswordDto.Email, "Password Reset", url);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<AppUser>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token)), resetPasswordDto.NewPassword);

            if (!result.Succeeded) return Unauthorized(result.Errors);
                return user;
        }
    }
}
