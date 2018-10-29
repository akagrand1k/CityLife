using System.Threading.Tasks;
using CityLife.Authenticate.Extends.Senders;
using CityLife.DALImplementation.Context;
using CityLife.Entities.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using Microsoft.Owin.Security.DataProtection;

namespace CityLife.Authenticate.Managers
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {

        }

        public static AppUserManager GetInstance(IdentityFactoryOptions<AppUserManager> option, IOwinContext owinContext)
        {

            var appContext = new AppContext();
            var usermgr = new AppUserManager(new UserStore<AppUser>(appContext));
            usermgr.PasswordValidator = new PasswordValidator
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true,
            };

            usermgr.UserValidator = new UserValidator<AppUser>(usermgr)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = true
            };
            var tokenProvider = new DpapiDataProtectionProvider("citylife");
            usermgr.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(tokenProvider.Create("EmailConfirmation"));
            usermgr.EmailService = new EmailSenderIdentity(EmailContentId,AttachFile);

            return usermgr;
        }

        public static string EmailContentId
        {
            get; set;
        }
        public static string AttachFile { get; set; }


    }
}

