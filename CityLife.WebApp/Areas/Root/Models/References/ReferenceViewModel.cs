using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CityLife.Entities.Models.References;
namespace CityLife.WebApp.Areas.Root.Models.References
{
    public class ApplicationReferenceViewModel : ApplicationReferences
    {
        [Required(ErrorMessage ="Введите наименование!")]
        public new string Name { get; set; }
    }
}