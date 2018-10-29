using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.Profile
{
    public class ProfileViewModel : Abstract.AbstractViewModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string BrithDate { get; set; }
        public string FirstPhone { get; set; }
        public string SecondPhone { get; set; }
        public string PicturePath { get; set; }
        public string SmallPicturePath { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string Alias { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }



        //Social
        public int CountUser { get; set; }
        public int CountCompany { get; set; }
        public int CountArticles { get; set; }
        public int CountExceptionToday { get; set; }
    }
}