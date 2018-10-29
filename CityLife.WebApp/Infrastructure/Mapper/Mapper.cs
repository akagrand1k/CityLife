using CityLife.Entities.Models.News;
using CityLife.Entities.Models.Organizations;
using CityLife.Entities.Models.References;
using CityLife.Entities.Models.Sections;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Areas.Administrator.Models.User;
using CityLife.WebApp.Areas.Moderator.Models.Organization;
using CityLife.WebApp.Areas.Root.Models.Classifier;
using CityLife.WebApp.Areas.Root.Models.References;
using CityLife.WebApp.Models.Account;
using CityLife.WebApp.Models.News;
using CityLife.WebApp.Models.Organization;
using CityLife.WebApp.Models.Profile;
using CityLife.WebApp.Models.Section;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Infrastructure.Mapper
{
    #region Users

    internal static class UserMapper
    {

        public static UserViewModel ToViewModel(AppUser entity)
        {
            return new UserViewModel
            {
                auth_via = entity.auth_via,
                social_id = entity.social_id,
                UserName = entity.UserName,
                DateCreate = entity.DateCreate,
                DateUpdate = entity.DateUpdate,
                DateLastAuth = entity.DateLastAuth,
                Id = entity.Id,
                Email = entity.Email,
                EmailConfirmed = entity.EmailConfirmed,
            };
        }

        public static AppUser ToEntity(UserViewModel model)
        {
            return new AppUser
            {
                Email = model.Email,
                auth_via = model.auth_via,
                social_id = model.social_id,
                UserName = model.Email,
                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
                Id = model.Id,
                EmailConfirmed = model.EmailConfirmed,
                DateLastAuth = model.DateLastAuth,
                DateCreate = model.DateCreate,
                DateUpdate = model.DateUpdate,
            };
        }

    }

    #endregion

    #region SysReferences

    internal static class SysReferencesMapper
    {
        public static ApplicationSysReferences ToEntity(ApplicationSysReferenceViewModel model)
        {
            return new ApplicationSysReferences
            {

                Key = model.Key,
                Value = model.Value,
                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
            };
        }

        public static ApplicationSysReferenceViewModel ToViewModel(ApplicationSysReferences entity)
        {
            return new ApplicationSysReferenceViewModel
            {
                Key = entity.Key,
                Value = entity.Value,
                IsDelete = entity.IsDelete,
                IsActive = entity.IsActive,
            };
        }
    }

    #endregion

    #region References
    internal static class ReferencesMapper
    {
        public static ApplicationReferences ToEntity(ApplicationReferenceViewModel model)
        {
            return new ApplicationReferences
            {
                Id = model.Id,
                Alias = model.Alias,
                Key = model.Key,
                ParentId = model.ParentId,
                Name = model.Name,
                Value = model.Value,
                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
                IsParent = model.IsParent
            };
        }

        public static ApplicationReferenceViewModel ToViewModel(ApplicationReferences entity)
        {
            return new ApplicationReferenceViewModel
            {
                Id = entity.Id,
                Alias = entity.Alias,
                Key = entity.Key,
                ParentId = entity.ParentId,
                Name = entity.Name,
                Value = entity.Value,
                IsParent = entity.IsParent,
                IsDelete = entity.IsDelete,
                IsActive = entity.IsActive,
            };
        }
    }
    #endregion

    #region Organization
    internal static class OrganizationMapper
    {
        public static Organization ToEntity(OrganizationViewModel model)
        {
            Organization entity = new Organization();

            entity.LargePathImage = model.LargePathImage;
            entity.SmallPathImage = model.SmallPathImage;
            entity.Name = model.Name;
            entity.ShortDescription = model.ShortDescription;
            entity.Alias = model.Alias;
            entity.IsActive = model.IsActive;
            entity.ClassifierId = model.ClassifierId;
            entity.DateCreate = model.DateCreate;
            entity.DetailsDescription = model.DetailsDescription;
            entity.HeightImg = model.HeightImg;
            entity.WidthImg = model.WidthImg;
            entity.Id = model.Id;
            entity.IsDelete = model.IsDelete;
            entity.OwnerId = model.OwnerId;
            entity.Building = model.Building;
            entity.Street = model.Street;
            entity.Home = model.Home;
            entity.Longitude = model.Longitude;
            entity.Latitude = model.Latitude;

            return entity;
        }

        public static Organization ToEntity(OrgRegisterViewModel model)
        {
            Organization entity = new Organization();

            entity.LargePathImage = model.LargePathImage;
            entity.SmallPathImage = model.SmallPathImage;
            entity.Name = model.Name;
            entity.ShortDescription = model.ShortDescription;
            entity.Alias = model.Alias;
            entity.IsActive = model.IsActive;
            entity.ClassifierId = model.ClassifierId;
            entity.DateCreate = model.DateCreate;
            entity.DetailsDescription = model.DetailsDescription;
            entity.HeightImg = model.HeightImg;
            entity.WidthImg = model.WidthImg;
            entity.Id = model.Id;
            entity.IsDelete = model.IsDelete;
            entity.OwnerId = model.OwnerId;
            entity.Building = model.Building;
            entity.Street = model.Street;
            entity.Home = model.Home;
            entity.Longitude = decimal.Parse(model.Longitude, CultureInfo.InvariantCulture.NumberFormat);
            entity.Latitude = decimal.Parse(model.Latitude, CultureInfo.InvariantCulture.NumberFormat);
            entity.Notation = model.Notation;

            return entity;
        }

        public static OrganizationViewModel ToViewModel(Organization entity)
        {
            OrganizationViewModel model = new OrganizationViewModel();

            if (!string.IsNullOrEmpty(entity.LargePathImage))
                model.LargePathImage = AppConstants.directoryProfileAvatar + entity.LargePathImage;

            if (!string.IsNullOrEmpty(entity.SmallPathImage))
                model.SmallPathImage = AppConstants.directoryProfileAvatar + entity.SmallPathImage;

            model.Name = entity.Name;
            model.ShortDescription = entity.ShortDescription;
            model.Alias = entity.Alias;
            model.IsActive = entity.IsActive;
            model.ClassifierId = entity.ClassifierId;
            model.DateCreate = entity.DateCreate;
            model.DetailsDescription = entity.DetailsDescription;
            model.HeightImg = entity.HeightImg;
            model.WidthImg = entity.WidthImg;
            model.Id = entity.Id;
            model.IsDelete = entity.IsDelete;
            model.OwnerId = entity.OwnerId;
            model.Classifier = entity.Classifier;
            model.Owner = entity.Owner;
            model.Building = entity.Building;
            model.Street = entity.Street;
            model.Home = entity.Home;
            model.Longitude = entity.Longitude;
            model.Latitude = entity.Latitude;
            return model;
        }

        public static OrgRegisterViewModel ToRegisterViewModel(Organization entity)
        {

            OrgRegisterViewModel model = new OrgRegisterViewModel();

            if (!string.IsNullOrEmpty(entity.LargePathImage))
                model.LargePathImage = AppConstants.directoryProfileAvatar + entity.LargePathImage;

            if (!string.IsNullOrEmpty(entity.SmallPathImage))
                model.SmallPathImage = AppConstants.directoryProfileAvatar + entity.SmallPathImage;

            model.Name = entity.Name;
            model.ShortDescription = entity.ShortDescription;
            model.Alias = entity.Alias;
            model.IsActive = entity.IsActive;
            model.ClassifierId = entity.ClassifierId;
            model.DateCreate = entity.DateCreate;
            model.DetailsDescription = entity.DetailsDescription;
            model.HeightImg = entity.HeightImg;
            model.WidthImg = entity.WidthImg;
            model.Id = entity.Id;
            model.IsDelete = entity.IsDelete;
            model.OwnerId = entity.OwnerId;
            model.Classifier = entity.Classifier;
            model.Owner = entity.Owner;
            model.Building = entity.Building;
            model.Street = entity.Street;
            model.Home = entity.Home;
            model.Longitude = entity.Longitude.ToString();
            model.Latitude = entity.Latitude.ToString();
            return model;
        }
    }
    #endregion

    #region ClassifierOrg
    internal static class ClassifierMapper
    {
        public static ClassifierOrganization ToEntity(ClassifierViewModel model)
        {
            return new ClassifierOrganization
            {
                Alias = model.Alias,
                Id = model.Id,
                DateCreate = model.DateCreate,
                Description = model.Description,
                IsActive = model.IsActive,
                Name = model.Name,
                IsDelete = model.IsDelete,
            };
        }

        public static ClassifierViewModel ToViewModel(ClassifierOrganization entity)
        {
            return new ClassifierViewModel
            {
                Alias = entity.Alias,
                Id = entity.Id,
                DateCreate = entity.DateCreate,
                Description = entity.Description,
                IsActive = entity.IsActive,
                Name = entity.Name,
                IsDelete = entity.IsDelete,
            };
        }
    }
    #endregion

    #region TemplatePages

    internal static class RootTemplatePagesMapper
    {
        //public static ToEntity()       
    }


    #endregion

    #region Section

    internal static class SectionMapper
    {

        public static Section ToEntity(SectionViewModel model)
        {
            return new Section
            {
                Alias = model.Alias,

                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
                Id = model.Id ?? 0,
                Name = model.Name,
                ObsoleteUrl = model.ObsoleteUrl,
                Url = model.Url,
                Weight = model.Weight

            };
        }

        public static SectionViewModel ToViewModel(Section model)
        {
            return new SectionViewModel
            {
                Alias = model.Alias,
                DateCreate = model.DateCreate,
                DateUpdate = model.DateUpdate ?? DateTime.MinValue,
                IsActive = model.IsActive,
                IsDelete = model.IsDelete,
                Id = model.Id,
                Name = model.Name,
                ObsoleteUrl = model.ObsoleteUrl,
                Url = model.Url,
                Weight = model.Weight

            };

        }

    }

    #endregion

    #region News

    public static class NewsMapper
    {
        public static NewsViewModel ToViewModel(Article entity)
        {
            return new NewsViewModel
            {
                ArticleId = entity.Id,
                Content = entity.Content,
                IsActive = entity.IsActive,
                OwnerId = entity.OwnerId == null ? 0 : entity.OwnerId.Value,
                IsDelete = entity.IsDelete,
                Name = entity.Name,
                ShortDescript = entity.ShortDescription,
                Tags = entity.Tags,
                PreviewPic = entity.PreviewPictures != null ? entity.PreviewPictures.FirstOrDefault() : null,
                PreviewPictures = entity.PreviewPictures,
                DateCreate = entity.DateCreate,
                DateUpdate = entity.DateUpdate == null ? DateTime.MinValue : entity.DateUpdate.Value
            };
        }

        public static Article ToEntity(NewsViewModel model)
        {
            return new Article
            {
                Id = model.ArticleId,
                Content = model.Content,
                IsActive = model.IsActive,
                OwnerId = model.OwnerId,
                IsDelete = model.IsDelete,
                Name = model.Name,
                ShortDescription = model.ShortDescript,
            };
        }
    }

    #endregion

    #region Articles

    public static class ArticleMapper
    {
        public static ArticleViewModel ToViewModel(Article entity)
        {
            ArticleViewModel model = new ArticleViewModel();
            model.Alias = entity.Alias;
            model.AutorName = entity.Owner != null ? $"{entity.Owner.GivenName} {entity.Owner.FamilyName}" : "";
            model.Content = entity.Content;
            model.DateCreate = entity.DateCreate;
            model.OwnerId = entity.OwnerId;
            model.PathAutorAvatar = entity.Owner != null ? entity.Owner.PicturePath : "";
            model.Id = entity.Id;
            model.PathPreview = entity.PreviewPictures != null ?
                (entity.PreviewPictures.FirstOrDefault() != null ? entity.PreviewPictures.FirstOrDefault().SmallSizePath : "") : "";
            model.PathTitlePic = entity.PreviewPictures != null ?
                (entity.PreviewPictures.FirstOrDefault() != null ? entity.PreviewPictures.FirstOrDefault().LargeSizePath : "") : "";
            model.ShortDescript = entity.ShortDescription;
            model.Tags = entity.Tags != null ? string.Join(", ", entity.Tags.Select(x => x.Name)) : "";
            model.Title = entity.Name;
            model.TagsList = entity.Tags;
            return model;
        }

    }
    #endregion

    public static class UserProfileMapper
    {
        public static UserProfile ToEntity(OrgRegisterViewModel model)
        {
            return new UserProfile
            {
                GivenName = model.Email,
                PicturePath = model.SmallPathImage,
                UserId = model.OwnerId,
                IsActive = true,
            };
        }

        public static UserProfile ToEntity(RegisterViewModel model)
        {
            return new UserProfile
            {
                GivenName = model.Email,
                UserId = model.OwnerId,
                IsActive = true,
            };
        }

        public static UserProfile ToEntity(ProfileViewModel model)
        {
            return new UserProfile
            {
                GivenName = model.GivenName,
                Alias = model.Alias,
                IsActive = model.IsActive,
                BrithDate = Convert.ToDateTime(model.BrithDate),
                PicturePath = model.PicturePath,
                FamilyName = model.FamilyName,
                Name = model.Name,
                SecondPhone = model.SecondPhone,
                FirstPhone = model.FirstPhone,
                DateCreate = model.DateCreate,
                IsDelete = model.IsDelete,
            };
        }

        public static ProfileViewModel ToViewModel(UserProfile profile)
        {
            ProfileViewModel model = new ProfileViewModel();           
            if (!string.IsNullOrEmpty(profile.GivenName))
                model.GivenName = profile.GivenName;
            if (!string.IsNullOrEmpty(profile.Alias))
                model.Alias = profile.Alias;
            model.IsActive = profile.IsActive;
            if (profile.BrithDate != null)
                model.BrithDate = profile.BrithDate.Value.Date.ToString("dd.MM.yyyy");

            if (profile.IsExternal)
            {
                if (profile.PicturePath != null)
                    model.PicturePath = AppConstants.directoryProfileAvatar + profile.PicturePath;
                else
                    model.PicturePath = profile.ExternalPic;
            }
            else
            {
                model.PicturePath = AppConstants.directoryProfileAvatar + profile.PicturePath;
            }
            
            model.FamilyName = profile.FamilyName;
            model.Name = profile.Name;
            model.FirstPhone = profile.FirstPhone;
            model.SecondPhone = profile.SecondPhone;
            model.DateCreate = profile.DateCreate;
            model.IsDelete = profile.IsDelete;
            if (profile.User!= null)
                model.Email = profile.User.Email;

            return model;
        }
    }
}