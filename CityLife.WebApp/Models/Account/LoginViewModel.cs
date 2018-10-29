using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public IEnumerable<AuthenticationDescription> ExternalProvider { get; set; }
       
    }

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }        
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "Введите Email!")]
        public string Email { get; set; }
        public bool ExternLoginSucces { get; set; }
    }
}