using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Укажите пароль!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
        public bool ExternLoginSucces { get; set; }

        public string OwnerId { get; set; }
    }
}