using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreWebApiApp.Services
{
    public class AuthService
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
    }
}
