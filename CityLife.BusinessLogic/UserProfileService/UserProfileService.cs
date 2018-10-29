using System;
using System.Collections.Generic;
using System.Linq;
using CityLife.Entities.Models.User;
using CityLife.DataAccess.Interfaces;
using CityLife.Extensions.ExtensionMethods;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.Extensions.Constant;
using System.Data.Entity;
namespace CityLife.BusinessLogic.UserProfileService
{
    public class UserProfileService : IUserProfileService
    {
        private readonly string section = "Профиль пользователя";

        private readonly IRepository<UserProfile> repository;
        private readonly IUnitOfWork unitOfWork;
        public UserProfileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("parameter: unitOfwork");
        }

        public IExceptionService ExceptionService
        {
            get
            {
                return new ExceptionService(unitOfWork);
            }
        }

        public IEnumerable<UserProfile> Profiles
        {
            get
            {
                try
                {
                    IEnumerable<UserProfile> result = new List<UserProfile>();
                    var ctx = unitOfWork.CityLifeDbContext;

                    result = ctx
                        .Set<UserProfile>()
                        .ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                    throw new Exception(ex.Message);
                }
            }
        }

        public UserProfile Edit(UserProfile entry)
        {
            try
            {
                UserProfile entity = new UserProfile();
                var ctx = unitOfWork.CityLifeDbContext;
                if (entry.Id > 0)
                {
                    entity = ctx.Set<UserProfile>()
                        .FirstOrDefault(x => x.Id == entry.Id);
                }
                else
                {
                    ctx.Set<UserProfile>()
                        .Add(entity);
                }

                entity.IsActive = entry.IsActive;
                entity.IsDelete = entry.IsDelete;
                entity.GivenName = entry.GivenName;
                entity.FamilyName = entry.FamilyName;
                entity.BrithDate = entry.BrithDate;
                entity.Alias = $"{entry.GivenName} {entry.FamilyName}".ToAlias();
                entity.Name = $"{entry.GivenName} {entry.FamilyName}";
                entity.FirstPhone = entry.FirstPhone;
                entity.SecondPhone = entry.SecondPhone;
                entity.UserId = entry.UserId;
                entity.PicturePath = entry.PicturePath;

                ctx.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }

        public UserProfile Edit(UserProfile entry, string userId)
        {
            try
            {
                UserProfile entity = new UserProfile();
                var ctx = unitOfWork.CityLifeDbContext;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("userId is null or empty!");
                }

                entity = ctx.Set<UserProfile>()
                    .FirstOrDefault(x => x.UserId == userId);

                entity.GivenName = entry.GivenName;
                entity.FamilyName = entry.FamilyName;
                if (entry.BrithDate != null)
                    entity.BrithDate = entry.BrithDate;

                entity.FirstPhone = entry.FirstPhone;
                entity.SecondPhone = entry.SecondPhone;

                ctx.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }

        public UserProfile Get(string userId)
        {
            try
            {
                UserProfile profile = null;
                var ctx = unitOfWork.CityLifeDbContext;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("userId is null or empty!");
                }

                profile = ctx.Set<UserProfile>()
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.UserId == userId);
                return profile;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }

        public UserProfile Get(int id)
        {
            try
            {
                UserProfile profile = null;

                var ctx = unitOfWork.CityLifeDbContext;

                if (id > 0)
                {
                    profile = ctx.Set<UserProfile>()
                    .FirstOrDefault(x => x.Id == id);
                }

                return profile;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<UserProfile> Get(Func<UserProfile, bool> predicate)
        {
            try
            {
                IEnumerable<UserProfile> source = new List<UserProfile>();

                var ctx = unitOfWork.CityLifeDbContext;

                source = ctx.Set<UserProfile>().Where(predicate).ToList();

                return source;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }

        public bool Remove(int id)
        {
            try
            {
                UserProfile profile = repository.Get(id);

                var ctx = unitOfWork.CityLifeDbContext;
                if (id == 0)
                {
                    throw new Exception("Arg id is requeired!");
                }
                profile = ctx.Set<UserProfile>()
                    .FirstOrDefault(x => x.Id == id);

                profile.IsActive = false;
                profile.IsDelete = true;

                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }

        public bool UploadAvatar(string path, AppUser user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException("One or much more parameters is null!");
                }

                var ctx = unitOfWork.CityLifeDbContext;
                var profile = ctx.Set<UserProfile>()
                    .FirstOrDefault(x => x.UserId == user.Id);

                profile.PicturePath = path;
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_UserProfile);
                throw new Exception(ex.Message);
            }
        }
    }
}