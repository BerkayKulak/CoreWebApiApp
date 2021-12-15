using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWebApiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoreWebApiApp.Services
{
    public class AuthService
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private IConfiguration configuration;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

      
        public async Task<bool> CreateUserAsync(RegisterUser user)
        {
            bool isUserCreated = false;
            var newUser = new IdentityUser()
            {
                UserName = user.Email,
                Email = user.Email
                
            };
            var res =await userManager.CreateAsync(newUser,user.Password);
            if (res.Succeeded)
            {
                isUserCreated = true;
            }
            return isUserCreated;
        }

        public async Task<string> AuthUserAsync(LoginUser user)
        {
            string jwtToken = "";
            var result =
                await signInManager.PasswordSignInAsync(user.UserName, user.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var secretKey = Convert.FromBase64String(configuration["JWTCoreSettings.SecretKey"]);
                var expiryTime = Convert.ToInt32(configuration["JWTCoreSettings.ExpiryInMinuts"]);
                IdentityUser appUser = new IdentityUser(user.UserName);
                // Describe the JWT Token
                var securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = null,
                    Audience = null,
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("username",appUser.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expiryTime),
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),SecurityAlgorithms.HmacSha256Signature)
                   
                };
            }
            else
            {
                jwtToken = "Login Failed!";
            }
            return jwtToken;
        }
    }
}
