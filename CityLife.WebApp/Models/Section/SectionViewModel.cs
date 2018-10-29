using CityLife.WebApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Section
{
    public class SectionViewModel:AbstractViewModel
    {

        public string CurrentRole { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ObsoleteUrl { get; set; }
       
        public int Weight { get; set; }
    }
}