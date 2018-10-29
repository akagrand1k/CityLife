using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Organization.JSON
{
    public class AutoCompleteJSON
    {
        public AutoCompleteJSON()
        {
            query = "Unit";
            suggestions = new List<Suggestions>();
        }

        public string query { get; set; }
        public List<Suggestions> suggestions { get; set; }
    }
    public class Suggestions
    {
        public string value { get; set; }
        public string data { get; set; }

        public string searchCritery { get; set; }
    }
}