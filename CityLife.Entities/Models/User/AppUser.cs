using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.User
{
    public class AppUser :IdentityUser
    {
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string social_id { get; set; }
        public string auth_via { get; set; }
        public DateTime? DateLastAuth { get; set; }
        public string GoogleToken { get; set; }
        public string FacebookToken { get; set; }
        public string VkToken { get; set; }

        public AppUser()
        {
            DateCreate = DateTime.Now;
            IsActive = true;
        }

        public async Task <ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> userManager)
        {
            var userIdentity =
                await
                userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
