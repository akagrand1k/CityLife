using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Administrator.Models.User
{
    public class ChangePassViewModel
    {
        [Required(ErrorMessage = "Укажите пароль!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }

        public string Id { get; set; }
    }
}