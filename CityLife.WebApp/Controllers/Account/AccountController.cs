//#define _RELEASE
#define _DEBUG
using CityLife.Authenticate.Managers;
using CityLife.Authenticate.Options;
using CityLife.BusinessLogic.ClassifierService;
using CityLife.BusinessLogic.OrganizationService;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Infrastructure.ExtendViewEngine.Handler;
using CityLife.WebApp.Infrastructure.ExternalLogin;
using CityLife.WebApp.Infrastructure.ImageResize;
using CityLife.WebApp.Infrastructure.Mapper;
using CityLife.WebApp.Models;
using CityLife.WebApp.Models.Account;
using CityLife.WebApp.Models.Organization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Nemiro.OAuth;
using Nemiro.OAuth.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Controllers
{
    public class AccountController : BaseController, IDisposable
    {
        private readonly IUserProfileService profileService;
        private readonly IOrganizationService orgService;
        private readonly IClassifierService classifierService;
        private readonly IAuthUserInfo<UserProfile> authUserInfo;

        public AccountController(IAuthUserInfo<UserProfile> authUserInfo,
            IOrganizationService _orgService, IClassifierService _classifierService, IUserProfileService _userSrv) : base(_userSrv)
        {
            this.authUserInfo = authUserInfo;
            this.orgService = _orgService;
            this.classifierService = _classifierService;
            this.profileService = _userSrv;
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
        }


        #region Standard app authentication

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(RoleConstant.RoleRoot))
                    return RedirectToAction("Index", "Home", new { area = "Root" });

                if (User.IsInRole(RoleConstant.RoleAdmin))
                    return RedirectToAction("Index", "Home", new { area = "Administrator" });

                if (User.IsInRole(RoleConstant.RoleModerator))
                    return RedirectToAction("Index", "Home", new { area = "Moderator" });
            }

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProvider = HttpContext
                .GetOwinContext().Authentication.GetExternalAuthenticationTypes()
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            model.ExternalProvider = HttpContext
                .GetOwinContext().Authentication.GetExternalAuthenticationTypes();

            if (ModelState.IsValid)
            {
                var authUser = UserManager.Find(model.Email, model.Password);

                if (authUser == null)
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
                else
                {
                    if (!authUser.IsActive)
                        ModelState.AddModelError("", "Аккаунт заблокирован");
                    else
                    {
                        ClaimsIdentity clIdent = UserManager.CreateIdentity(authUser, DefaultAuthenticationTypes.ApplicationCookie);

                        authUser.DateLastAuth = DateTime.Now;
                        var res = UserManager.UpdateAsync(authUser).Result;

                        AuthManager.SignOut();
                        AuthManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe, AllowRefresh = true }, clIdent);


                        var userProf = profileService.Get(authUser.Id);

                        if (userProf == null)
                        {
                            profileService.Edit(new UserProfile { UserId = authUser.Id, GivenName = authUser.Email });
                        }

                        if (User.IsInRole(RoleConstant.RoleRoot))
                            return Redirect("~/Root/Home");

                        if (User.IsInRole(RoleConstant.RoleAdmin))
                            return Redirect("~/Administrator/Home");

                        if (User.IsInRole(RoleConstant.RoleModerator))
                            return Redirect("~/Moderator/Home");

                        return Redirect(string.IsNullOrEmpty(model.ReturnUrl) ? "~/" : model.ReturnUrl);
                    }
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                LockoutEnabled = true,
                LockoutEndDateUtc = DateTime.Now.AddHours(7)
            };

            IdentityResult identResult = UserManager.Create(user, model.Password);
            if (!identResult.Succeeded)
            {
                AddErrorsFromResult(identResult);
                return View(model);
            }
            else
            {
#if _DEBUG


                model.OwnerId = user.Id;
                profileService.Edit(UserProfileMapper.ToEntity(model));
                UserManager.AddToRole(user.Id, RoleConstant.RoleUser);
                SignInManager.SignIn(user, model.RememberMe, false);
                identResult = UserManager.Update(user);
                if (!identResult.Succeeded)
                {
                    AddErrorsFromResult(identResult);
                    return View(model);
                }
                return Redirect("~/user/profile");



#endif

#if _RELEASE
                string token = UserManager.GenerateEmailConfirmationToken(user.Id);
                SendEmail(new EmailVerify
                {
                    CallbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }),
                    cid = Guid.NewGuid().ToString(),
                    UserName = user.UserName
                }, user.Id);

                return View("TwoFactorMessage");
#endif
            }

        }

        public ActionResult ConfirmEmail(string userId, string token)
        {
            if (String.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return View();
            }

            var result = UserManager.ConfirmEmail(userId, token);

            if (result.Succeeded)
            {
                UserManager.AddToRole(userId, RoleConstant.RoleUser);
                return Redirect("~/");
            }

            return View();

        }

        #endregion

        #region External authentication

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCollback", "Account", new { returnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginCollback(string returnUrl)
        {
            var loginInfo = AuthManager.GetExternalLoginInfo();

            if (loginInfo == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = SignInManager.ExternalSignIn(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:

                    var user = UserManager.FindByName(loginInfo.Email);

                    if (user != null)
                    {
                        user.DateLastAuth = DateTime.Now;
                        UserManager.Update(user);
                    }

                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:

                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    //TODO Implemetn verifycation account by code
                    break;
                case SignInStatus.Failure:

                default:

                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    var model = new ExternalLoginViewModel
                    {
                        Email = loginInfo.Email,
                        Name = loginInfo.ExternalIdentity.Name,
                        ExternLoginSucces = true,
                    };
                    return View("ExternalLoginConfirmation", model);
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl)
        {
            var appUser = new AppUser();

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal("~/");
            }

            if (ModelState.IsValid)
            {
                var loginInfo = AuthManager.GetExternalLoginInfo();

                if (loginInfo == null)
                {
                    model.ExternLoginSucces = false;
                    return View(model);
                }

                if (!string.IsNullOrEmpty(model.Email))
                {
                    appUser.UserName = model.Email;
                    appUser.Email = model.Email;
                }
                else
                {
                    model.ExternLoginSucces = false;
                    return View(model);
                }

                var result = UserManager.Create(appUser);

                if (result.Succeeded)
                {
                    bool res = authUserInfo.AddInfo(loginInfo, appUser.Id, loginInfo.Login.LoginProvider);

                    if (!res)
                    {
                        model.ExternLoginSucces = res;
                        ModelState.AddModelError("", "Во время сохранения аккаунта возникли ошибки!");
                        return View("ExternalLoginConfirmation", model);
                    }

                    result = UserManager.AddLogin(appUser.Id, loginInfo.Login);

                    if (result.Succeeded)
                    {
                        appUser.EmailConfirmed = true;
                        UserManager.AddToRole(appUser.Id, RoleConstant.RoleUser);
                        result = UserManager.Update(appUser);

                        if (result.Succeeded)
                        {
                            SignInManager.SignIn(appUser, false, false);
                        }
                        else
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Во время регистрации возникли ошибки!");
                return View("ExternalLoginConfirmation", model);
            }

            return RedirectToLocal("~/");
        }


        #endregion

        #region Company authentication
        public ActionResult Company()
        {
            ViewBag.ListСlassifikate = new SelectList(classifierService.GetAll.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Company(OrgRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();

                ViewBag.ListСlassifikate = new SelectList(classifierService.GetAll.ToList(), "Id", "Name");
                return View(model);
            }

            AppUser user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                LockoutEnabled = true,
                LockoutEndDateUtc = DateTime.Now.AddHours(7)
            };




            IdentityResult identResult = UserManager.Create(user, model.Password);
            if (!identResult.Succeeded)
            {
                AddErrorsFromResult(identResult);
                ViewBag.ListСlassifikate = new SelectList(classifierService.GetAll.ToList(), "Id", "Name");
                return View(model);
            }
            model.SmallPathImage = ImageResize.Resize(model.LogoFile, AppConstants.directoryProfileAvatar, 40, 40);
            model.LargePathImage = ImageResize.Resize(model.LogoFile, AppConstants.directoryProfileAvatar, 135, 135);
            model.OwnerId = user.Id;
            UserManager.AddToRole(user.Id, RoleConstant.RoleCompany);
            orgService.Edit(OrganizationMapper.ToEntity(model));
            profileService.Edit(UserProfileMapper.ToEntity(model));



#if _DEBUG
            SignInManager.SignIn(user, model.RememberMe, false);
            return Redirect("~/");
#endif

#if _RELEASE
                string token = UserManager.GenerateEmailConfirmationToken(user.Id);
                SendEmail(new EmailVerify
                {
                    CallbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }),
                    cid = Guid.NewGuid().ToString(),
                    UserName = user.UserName
                }, user.Id);

                return View("TwoFactorMessage");
#endif

        }
        #endregion

        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("~/");
        }

        #region Support methods

        [NonAction]
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        private ActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }

            return RedirectToAction("Index", "Home");
        }

        private void SendEmail(EmailVerify model, string userId)
        {
            string body = InitializerMail.GetHtmlTemplateString<EmailVerify>("~/Views/Shared/EmailVerify.cshtml", model);

            UserManager.SendEmailAsync(userId, "Подтверждение электронной почты", body);
        }





        #endregion

        #region Obsolete methods


        [Obsolete]
        [NonAction]
        private bool CheckExistOAuthUser(AuthorizationResult authResult)
        {
            return UserManager.Users.Any(x => x.auth_via == authResult.ProviderName & x.social_id == authResult.UserInfo.UserId);
        }

        [Obsolete("Old method")]
        public ActionResult OAuthLogin(string provider)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string returnUrl = Url.Action("OAuthRegister", "Account", null, null, Request.Url.Host);
            return Redirect(OAuthWeb.GetAuthorizationUrl(provider, returnUrl));
        }

        [Obsolete("Old method")]
        public ActionResult OAuthRegister()
        {

            var result = OAuthWeb.VerifyAuthorization();
            if (result.IsSuccessfully)
            {
                var userInfo = result.UserInfo;

                AppUser appUser = new AppUser
                {
                    Email = userInfo.Email,
                    social_id = userInfo.UserId,
                    auth_via = result.ProviderName,
                    UserName = userInfo.Email,
                    LockoutEnabled = true,
                    LockoutEndDateUtc = DateTime.Now.AddHours(7)
                };

                if (!CheckExistOAuthUser(result))
                {
                    var resultIdentity = UserManager.Create(appUser);
                }

                //SignInManager.SignIn(appUser, true, true);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }



        [Obsolete("Moved in CityLife.Authenticate")]
        private bool InitializeUserProfileInfo(ExternalLoginInfo loginInfo, string userId, string provider = null)
        {
            bool result = false;
            Claim claim = null;
            dynamic response = null;
            UserProfile profile = null;
            string targetUrl = "";

            try
            {
                if (string.IsNullOrEmpty(provider))
                {
                    provider = loginInfo.Login.LoginProvider;
                }

                if (provider.ToLower() == SysConstants.LoginProvider_Google)
                {

                    claim = GetClaim(loginInfo, SysConstants.AccessToken_Google);
                    var dict = new Dictionary<string, object>();

                    dict.Add("access_token", claim.Value);
                    targetUrl = $"https://www.googleapis.com/oauth2/v2/userinfo?access_token={claim.Value}";
                    response = ApiTransactions.GetExtraAuthData(targetUrl);

                    if (response.error != null)
                    {
                        //TODO error
                        return false;
                    }

                    profile = new UserProfile();

                    profile.GivenName = response.given_name;
                    profile.FamilyName = response.family_name;
                    profile.PicturePath = response.picture;
                    profile.UserId = userId;
                    profile.IsActive = true;

                    // profileService.Edit(profile);

                    result = true;
                }

                if (provider.ToLower() == SysConstants.LoginProvider_Facebook)
                {
                    result = true;

                    //TODO init facebook

                }

                if (provider.ToLower() == SysConstants.LoginProvider_Vk)
                {

                    var accessToken = GetClaim(loginInfo, SysConstants.AccessToken_Vk);
                    var vk_userId = GetClaim(loginInfo, "user_id");

                    targetUrl = $"https://api.vk.com/method/users.get?id={vk_userId}&access_token={accessToken.Value}&fields=photo_200";
                    response = ApiTransactions.GetExtraAuthData(targetUrl);

                    if (response.error_code != null)
                    {
                        //TODO response user about error
                        return false;
                    }

                    profile = new UserProfile();

                    //TODO init vk user profile

                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Obsolete("Moved in CityLife.Authenticate")]
        private Claim GetClaim(ExternalLoginInfo info, string accessTokenKey)
        {
            Claim claim = info.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == accessTokenKey);

            return claim;
        }

        #endregion


    }
}