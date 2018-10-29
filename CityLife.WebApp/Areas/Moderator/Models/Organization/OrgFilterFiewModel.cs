using CityLife.WebApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Administrator.Models.Organization
{
    public class OrgFilterFiewModel : AbstractViewModel
    {
        public string OrgName { get; set; }
        public string ShortDescription { get; set; }
    }
}