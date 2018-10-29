using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Organization
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            ListKlassifier = new List<string>();
        }

        public List<string> ListKlassifier { get; set; }
    }
}