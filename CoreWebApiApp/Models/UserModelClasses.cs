using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiApp.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Email is Must")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Must")]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }

    public class LoginUser
    {
        [Required(ErrorMessage = "UserName is Must")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Must")]
        public string Password { get; set; }
    }

    public class ResponseData
    {
        public string ResponseMessage { get; set; }

    }
}
