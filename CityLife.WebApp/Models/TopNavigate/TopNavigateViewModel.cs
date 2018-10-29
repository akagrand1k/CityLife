using CityLife.WebApp.Models.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.TopNavigate
{
    public class TopNavigateViewModel
    {
        public string CurrentUrl { get; set; }
        public IEnumerable<SectionViewModel> Sections { get; set; }
    }
}