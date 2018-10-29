using CityLife.Entities.Models.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Root.Models.Classifier
{
    public class ClassifierViewModel : ClassifierOrganization
    {
        [Required(ErrorMessage = "Введите наименование!")]
        public new string Name { get; set; }
    }
}