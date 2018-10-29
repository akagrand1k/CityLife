using CityLife.Entities.Models.Banners;
using CityLife.Entities.Models.CityEvents;
using CityLife.Entities.Models.Exceptions;
using CityLife.Entities.Models.News;
using CityLife.Entities.Models.Organizations;
using CityLife.Entities.Models.PageTemplates;
using CityLife.Entities.Models.Pictures;
using CityLife.Entities.Models.Posters;
using CityLife.Entities.Models.References;
using CityLife.Entities.Models.Sections;
using CityLife.Entities.Models.User;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.DALImplementation.Context
{
    public partial class AppContext : IdentityDbContext<AppUser>
    {
        public const string connectionName = "CityLifeConnection";
        public AppContext() : base(connectionName)
        {
            Database.SetInitializer<AppContext>(new MigrateDatabaseToLatestVersion<AppContext, CityLife.DALImplementation.Migrations.Configuration>());
        }

       
        public DbSet<ApplicationSysReferences> ApplicationSysReferences { get; set; }
        public DbSet<ApplicationReferences> ApplicationReferences { get; set; }
        public DbSet<CityEvent> CityEvent { get; set; }
        public DbSet<ClassifierOrganization> ClassifierOrganization { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Poster> Poster { get; set; }
        public DbSet<PosterSchedule> PosterSchedule { get; set; }
        public DbSet<PageTextTemplate> PageTextTemplate { get; set; }
        public DbSet<UserProfile> UserPofile { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<AppExpection> AppException { get; set; }

        /// <summary>
        ///News preview  pics
        /// </summary>
        public DbSet<PreviewPictures> PreviewPictures { get; set; }

        /// <summary>
        /// Content pics for all pages.
        /// </summary>
        public DbSet<ArticlePicture> ArticlePicture { get; set; }

        public DbSet<ActionLog> ActionLog { get; set; }

        #region Banners
        public DbSet<BannerFile> BannerFile { get; set; }
        public DbSet<BannerOrder> BannerOrder { get; set; }
        public DbSet<BannerSchedule> BannerSchedule { get; set; }
        public DbSet<Checkout> Checkout { get; set; }

        #endregion
    }

}
