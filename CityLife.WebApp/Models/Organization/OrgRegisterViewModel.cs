using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Organization
{
    public class OrgRegisterViewModel : CityLife.Entities.Models.Organizations.Organization
    {
        [Required(ErrorMessage = "Введите наименование!")]
        public new string Name { get; set; }

        [Required(ErrorMessage ="Введите краткое описание!")]
        public new string ShortDescription { get; set; }
        [Required(ErrorMessage = "Введите подробное описание!")]
        public new string DetailsDescription { get; set; }


        [Required(ErrorMessage ="Введите улицу!")]
        public new string Street { get; set; }
        [Required(ErrorMessage ="Введите дом!")]
        public new string Home { get; set; }
        public new string Building { get; set; }
        
        [Upload("jpeg,jpg,png",5)]
        [Required(ErrorMessage ="Выберите файл!")]
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "Укажите Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Выберите классификатор!")]
        public new int ClassifierId { get; set; }

        public new string Latitude { get; set; }
        public new string Longitude { get; set; }
        
        public bool RememberMe { get; set; }
    }
}