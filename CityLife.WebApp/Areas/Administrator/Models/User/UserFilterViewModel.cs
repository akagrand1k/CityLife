using CityLife.WebApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Areas.Administrator.Models.User
{
    public class UserFilterViewModel : AbstractViewModel
    {
        public bool banCondition { get; set; }
    }
}