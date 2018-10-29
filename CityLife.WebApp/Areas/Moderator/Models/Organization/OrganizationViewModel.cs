using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Moderator.Models.Organization
{
    public class OrganizationViewModel : CityLife.Entities.Models.Organizations.Organization
    {
        [Required(ErrorMessage = "Введите наименование!")]
        public new string Name { get; set; }

        [Upload("jpg,jpeg,png",5)]
        public HttpPostedFileBase ImgFile { get; set; }

        [Required(ErrorMessage = "Выберите раздел")]
        public new int ClassifierId { get; set; }

        public Func<string,bool> IsInRole { get; set; }

        public string CurrentRole { get; set; }


    }
}