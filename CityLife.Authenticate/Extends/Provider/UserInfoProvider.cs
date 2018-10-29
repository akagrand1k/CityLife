using CityLife.Authenticate.Extends.OperationResult;
using CityLife.Authenticate.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.DALImplementation.Context;
using CityLife.Extensions.ExtensionMethods;

namespace CityLife.Authenticate.Extends.Provider
{
    public class UserInfoProvider : IAuthUserInfo<UserProfile>
    {

        private UserProfile profile;
        private GoogleUserInfo googleUserInfo;
        private VkUserInfo vkUserInfo;

        public UserInfoProvider()
        {

        }

        public bool AddInfo(ExternalLoginInfo loginInfo, string userId, string provider = null)
        {
            bool result = false;
            if (loginInfo == null)
            {
                throw new NullReferenceException("loginInfo");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new NullReferenceException("userId");
            }

            if (string.IsNullOrEmpty(provider))
            {
                provider = loginInfo.Login.LoginProvider;
            }

            if (provider.ToLower() == SysConstants.LoginProvider_Google)
            {
                result = InitInfoGoogleUser(loginInfo, userId);
            }

            if (provider.ToLower() == SysConstants.LoginProvider_Vk)
            {
                result = InitVkuser(loginInfo, userId);
            }

            return result;

        }

        public OperationDetails DeleteInfo(string userId)
        {
            return null;
        }


        public UserProfile Create(UserProfile user)
        {
            if (user == null)
            {
                throw new NullReferenceException("user");
            }

            if (string.IsNullOrEmpty(user.UserId))
            {
                throw new NullReferenceException("UserId");
            }

            UserProfile entity = new UserProfile();

            try
            {
                using (var ctx = new AppContext())
                {
                    user.Name = $"{user.GivenName} {user.FamilyName}";
                    entity.Alias = user.Name.ToAlias();
                    entity.BrithDate = user.BrithDate;
                    entity.FamilyName = user.FamilyName;
                    entity.GivenName = user.GivenName;
                    entity.ExternalPic = user.ExternalPic;
                    entity.UserId = user.UserId;
                    entity.FirstPhone = user.FirstPhone;
                    entity.SecondPhone = entity.SecondPhone;
                    entity.IsActive = true;
                    entity.IsExternal = true;
                    ctx.UserPofile.Add(entity);
                    ctx.SaveChanges();
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(string userId)
        {
            try
            {
                using (var ctx = new AppContext())
                {

                    var entity = ctx.UserPofile.FirstOrDefault(x => x.UserId == userId);

                    if (entity == null)
                    {
                        return false;
                    }

                    ctx.UserPofile.Remove(entity);
                    ctx.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        private bool InitInfoGoogleUser(ExternalLoginInfo loginInfo, string userId)
        {
            //bool result = false;
            googleUserInfo = new GoogleUserInfo();
            var response = googleUserInfo.InitializeInfo(loginInfo);

            profile =
                   new UserProfile
                   {
                       GivenName = response.FirstName,
                       FamilyName = response.LastName,
                       ExternalPic = response.Picture,
                       UserId = userId,
                   };

            //if (Create(profile) != null)
            //{
            //    result = true;
            //}

            return Create(profile) != null ? true : false;

            //return result;
        }

        private bool InitVkuser(ExternalLoginInfo loginInfo, string userId)
        {
            //bool result = false;
            vkUserInfo = new VkUserInfo();
            var response = vkUserInfo.InitializeUserProfileInfo(loginInfo);

            profile =
                new UserProfile
                {
                    GivenName = response.FirstName,
                    FamilyName = response.LastName,
                    ExternalPic = response.Picture,
                    UserId = userId,
                };

            //if (Create(profile) != null)
            //{
            //    result = true;
            //}

            return Create(profile) != null ? true : false;

            //return result;
        }
    }
}
