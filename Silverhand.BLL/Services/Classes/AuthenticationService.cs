using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Silverhand.BLL.Services.Interface;
using Silverhand.DAL.DTO.Requests;
using Silverhand.DAL.DTO.Responses;
using Silverhand.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;//crud db for users
        private readonly IConfiguration _configuration;//jwt
        private readonly IEmailSender _emailSender;//send email
        private readonly SignInManager<ApplicationUser> _signInManager;//signin users, validate password, logic, security

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
                throw new Exception("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);

            if (result.Succeeded)//inside signinmanager
            {
                return new UserResponse
                {
                    Token = await CreateTokenAsync(user)
                };
            }
            else if (result.IsLockedOut)
            {
                throw new Exception("Your account is locked");
            }
            else if (result.IsNotAllowed)
            {
                throw new Exception("Please confirm your email");
            }
            else
            {
                throw new Exception("Invalid email or password");
            }
        }

        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("User not found");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded ? "Email confirmed successfully" : "Email confirmation failed";
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest request)
        {
            var user = new ApplicationUser
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.UserName
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escape = Uri.EscapeDataString(token);//repmove special chars
                //build url
                var emailUrl =
                    $"{request.Scheme}://{request.Host}/api/Identity/Account/ConfirmEmail?token={escape}&userId={user.Id}";
                
                await _userManager.AddToRoleAsync(user, "Customer");

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Welcome to Silverhand",
                    $"<h1>Hello {user.UserName}</h1>" +
                    $"<a href='{emailUrl}'>Confirm your email</a>");

                // Here you could return a token, or simply something like in AYShop.
                return new UserResponse
                {
                    Token = registerRequest.Email
                };
            }

            throw new Exception(string.Join(";", result.Errors));
        }

        private async Task<string> CreateTokenAsync(ApplicationUser applicationUser)
        {
            // Create claims, which are pieces of information about the user
            var claims = new List<Claim>
            {
                new Claim("Name", applicationUser.UserName ?? ""),
                new Claim("Email", applicationUser.Email ?? ""),
                new Claim("Id", applicationUser.Id),
            };
            // Add roles as claims
            var roles = await _userManager.GetRolesAsync(applicationUser);
            foreach (var role in roles)
            {
                
                claims.Add(new Claim("Role", role));
            }
            // Create the security key, which will be used to sign the token, using the secret from configuration
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["jwtOptions:SecretKey"]));
            // Create signing credentials using the security key and a hashing algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Create the JWT token with claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(15),
                signingCredentials: credentials
            );
            // Return the serialized token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ForgotPassword(ForgetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new Exception("User not found");

            var random = new Random();
            var code = random.Next(1000, 9999).ToString();

            user.CodeResetPassword = code;
            user.CodeResetPasswordExpire = DateTime.UtcNow.AddMinutes(15);

            await _userManager.UpdateAsync(user);

            await _emailSender.SendEmailAsync(
                request.Email,
                "Reset password",
                $"<p>Your reset code is: <strong>{code}</strong></p>"
            );

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new Exception("User not found");

            if (user.CodeResetPassword != request.Code) return false;
            if (user.CodeResetPasswordExpire < DateTime.UtcNow) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(
                    request.Email,
                    "Password changed",
                    "<h1>Your password has been changed</h1>"
                );
            }

            return result.Succeeded;
        }
    }
}
