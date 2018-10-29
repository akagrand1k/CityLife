using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Widgets
{
    public class UserInfoViewModel
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}