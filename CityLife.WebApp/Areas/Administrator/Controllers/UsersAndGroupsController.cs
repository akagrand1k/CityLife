
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.Extensions.ExtensionMethods;
using CityLife.WebApp.Areas.Administrator.Models.User;
using CityLife.WebApp.Infrastructure.Mapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Threading.Tasks;

using System.Web.Mvc;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.BusinessLogic.UserProfileService;

namespace CityLife.WebApp.Areas.Administrator.Controllers
{
    [Roles(RoleConstant.RoleRoot, RoleConstant.RoleAdmin)]
    public class UsersAndGroupsController : DefaultController
    {
        #region Users


        public UsersAndGroupsController(IUserProfileService userSrv) : base(userSrv)
        {

        }

        public ActionResult Index()
        {
            ViewBag.CurrentRole = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            return View();
        }

        public ActionResult LockedUsers()
        {
            return View();
        }

        public async Task<ActionResult> Register(string id = "")
        {
            UserViewModel model = new UserViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var entity = await UserManager.FindByIdAsync(id);
                model = UserMapper.ToViewModel(entity);
            }
            var roles = RoleManager.Roles.ToList();

            ViewBag.Name = new SelectList(roles, "Name", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user = UserMapper.ToEntity(model);
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "UsersAndGroups");
                    else
                        ModelState.AddModelError("", "Что то пошло не так!");
                }
                else
                {
                    var entity = new AppUser();
                    entity.Email = model.Email;
                    entity.UserName = model.Email;
                    var result = await UserManager.CreateAsync(entity, model.Password);
                    if (result.Succeeded == false)
                    {
                        ModelState.AddModelError("", "Недопустимый пароль");
                        return View(model);
                    }
                    else
                    {
                        await this.UserManager.AddToRoleAsync(entity.Id, model.RoleName);
                        return RedirectToAction("Index", "UsersAndGroups");
                    }
                }
            }
            var roles = RoleManager.Roles.ToList();
            ViewBag.Name = new SelectList(roles, "Name", "Name");
            return View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            UserEditViewModel model = new UserEditViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                var entity = await UserManager.FindByIdAsync(id);
                model.Email = entity.Email;
            }

            var roles = RoleManager.Roles.ToList();

            ViewBag.Name = new SelectList(roles, "Name", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (user.Roles.Count != 0)
                        {
                            var oldRoleId = user.Roles.SingleOrDefault().RoleId;
                            var oldRoleName = RoleManager.Roles.SingleOrDefault(x => x.Id == oldRoleId).Name;
                            if (oldRoleName != model.RoleName)
                            {
                                await UserManager.RemoveFromRoleAsync(user.Id, oldRoleName);
                                await UserManager.AddToRoleAsync(user.Id, model.RoleName);

                                return RedirectToAction("Index", "UsersAndGroups");
                            }
                        }

                        else
                            await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                    }
                    else
                        ModelState.AddModelError("", "Что то пошло не так!");
                    return RedirectToAction("Index", "UsersAndGroups");
                }
            }
            var roles = RoleManager.Roles.ToList();
            ViewBag.Name = new SelectList(roles, "Name", "Name");
            return View(model);
        }


        public ActionResult ChangePassword(string id)
        {
            ChangePassViewModel model = new ChangePassViewModel() { Id = id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var removePassword = UserManager.RemovePassword(model.Id);
                if (removePassword.Succeeded)
                {
                    var AddPassword = UserManager.AddPassword(model.Id, model.Password);
                    if (AddPassword.Succeeded)
                    {
                        return RedirectToAction("Index", "UsersAndGroups");
                    }
                    else
                        ModelState.AddModelError("", "Недопустимый пароль!");
                }
            }
            return View(model);
        }


        public async Task<ActionResult> Ban(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                user.IsActive = false;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "UsersAndGroups");
            }
            return View();
        }


        public async Task<ActionResult> UnBan(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                user.IsActive = true;
                IdentityResult result = await UserManager.UpdateAsync(user);

                return RedirectToAction("LockedUsers", "UsersAndGroups");

            }
            return View();
        }
        #endregion


        #region JSON

        public JsonResult GetUsers(UserFilterViewModel model)
        {
            model.InitSortingData();
            var list = GetUsersList(model);

            var result = list.Select(x => new
            {
                ID = x.Id,
                Email = x.Email,
                DateCreate = x.DateCreate.GetFormatDateTime(),
                DateLastAuth = x.DateLastAuth.GetFormatDateTime(),
                RoleName = UserManager.GetRolesAsync(x.Id).Result,
                Status = x.IsActive ? "Активен" : "Забанен",
            });

            if (!User.IsInRole(RoleConstant.RoleRoot))
            {
                result = result.Where(x => x.RoleName.Any(y => y != "Root" && y != "Administrator"));
            }

            return Json(new { data = result, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region NonActionMethos

        [NonAction]
        private List<AppUser> GetUsersList(UserFilterViewModel model)
        {
            List<AppUser> entity = null;
            var result = UserManager.Users.Include(x => x.Roles).Where(x => x.IsActive == model.banCondition);
            result = result.OrderBy(x => x.Email);
            model.CountTotal = result.Count();
            entity = result.Skip(model.CountOnPage * (model.NumPage - 1)).Take(model.CountOnPage).ToList();
            return entity;
        }

        #endregion
    }
}
