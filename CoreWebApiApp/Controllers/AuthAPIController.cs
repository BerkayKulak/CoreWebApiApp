using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApiApp.Models;
using CoreWebApiApp.Services;

namespace CoreWebApiApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private AuthService service;

        public AuthAPIController(AuthService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await service.CreateUserAsync(user);
                if (isCreated == false)
                {
                    return Conflict($"User is already present {user.Email}");
                }

                var response = new ResponseData()
                {
                    ResponseMessage = $"{user.Email} User created successfully"
                };
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var token =await service.AuthUserAsync(user);
                if (token == null)
                {
                    return Unauthorized("The Authentication Failed");
                }

                var response = new ResponseData()
                {
                    ResponseMessage = token
                };
                return Ok(response);
            }
            return BadRequest(ModelState);
        }
    }
}
