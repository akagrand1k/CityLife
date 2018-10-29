using CityLife.Entities.Models.User;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;


namespace CityLife.Authenticate.Managers
{
    public class AppSignInManager: SignInManager<AppUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authManager) :base(userManager,authManager)
        {

        }

        public ClaimsIdentity CreateUserIdentity(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager).Result;
        }

        public static AppSignInManager CreateSignInManager(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
}
