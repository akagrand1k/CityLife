using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Administrator.Models.User
{
    public class UserEditViewModel : AppUser
    {
        [Required(ErrorMessage = "Укажите Email!")]
        public new string Email { get; set; }

        [Required(ErrorMessage = "Укажите роль!")]
        public string RoleName { get; set; }
    }
}