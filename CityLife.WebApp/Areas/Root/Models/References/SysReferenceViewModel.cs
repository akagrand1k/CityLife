using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Root.Models.References
{
    public class ApplicationSysReferenceViewModel: ApplicationSysReferences
    {
        [Required(ErrorMessage = "Введите значение!")]
        public new string Value { get; set; }
    }
}