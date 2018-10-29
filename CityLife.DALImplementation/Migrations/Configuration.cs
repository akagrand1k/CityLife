namespace CityLife.DALImplementation.Migrations
{
    using CityLife.DALImplementation.Context;
    using CityLife.Entities.Models.Organizations;
    using CityLife.Entities.Models.References;
    using CityLife.Extensions.Constant;
    using CityLife.Extensions.ExtensionMethods;
    using Entities.Models.User;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CityLife.DALImplementation.Context.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(CityLife.DALImplementation.Context.AppContext context)
        {
            UserInit(context);
            CompanyClassifierInit(context);
            ReferencesInit(context);      
        }

        private static void UserInit(AppContext context)
        {
            UserManager<AppUser> UM = new UserManager<AppUser>(new UserStore<AppUser>(context));
            RoleManager<IdentityRole> RM = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            IdentityRole role = null;

            try
            {
                role = new IdentityRole();
                role.Name = "Root";

                RM.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new IdentityRole();
                role.Name = "Administrator";
                RM.Create(role);
            }
            catch (Exception ex) { }


            try
            {
                role = new IdentityRole();
                role.Name = "Moderator";
                RM.Create(role);
            }
            catch (Exception ex) { }

            try
            {
                role = new IdentityRole();
                role.Name = "User";
                RM.Create(role);
            }
            catch { }

            try
            {
                role = new IdentityRole();
                role.Name = "Company";
                RM.Create(role);
            }
            catch { }

            try
            {
                AppUser User = new AppUser();
                User.UserName = "kamcitylife@yandex.ru";
                User.Email = "kamcitylife@yandex.ru";
                var res = UM.Create(User, "a!sads0185%!Sasd");
                if (res.Succeeded)
                {
                    UM.AddToRole(User.Id, "Root");

                }
            }
            catch (Exception ex) { }
        }

        private static void CompanyClassifierInit(AppContext context)
        {
            var item1 = new ClassifierOrganization {
                Name = "Государственные и муниципальные учреждения",
                Alias = "Государственные и муниципальные учреждения".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item1, context);

            var item2 = new ClassifierOrganization
            {
                Name = "Отдых и развлечения",
                Alias = "Отдых и развлечения".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item2, context);

            var item3 = new ClassifierOrganization
            {
                Name = "Культура и искусство",
                Alias = "Культура и искусство".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item3, context);

            var item4 = new ClassifierOrganization
            {
                Name = "Авто и мото",
                Alias = "Авто и мото".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item4, context);

            var item5 = new ClassifierOrganization
            {
                Name = "Бизнес и финансы",
                Alias = "Бизнес и финансы".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item5, context);

            var item6 = new ClassifierOrganization
            {
                Name = "Товары",
                Alias = "Товары".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item6, context);

            var item7 = new ClassifierOrganization
            {
                Name = "Наука и образование",
                Alias = "Наука и образование".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item7, context);

            var item8 = new ClassifierOrganization
            {
                Name = "Недвижимость и строительные организации",
                Alias = "Недвижимость и строительные организации".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item8, context);

            var item9 = new ClassifierOrganization
            {
                Name = "Спорт и туризм",
                Alias = "Спорт и туризм".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item9, context);

            var item10 = new ClassifierOrganization
            {
                Name = "Транспорт и перевозки",
                Alias = "Транспорт и перевозки".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item10, context);

            var item11 = new ClassifierOrganization
            {
                Name = "IT. Интернет. Компьютеры. Оргтехника",
                Alias = "IT. Интернет. Компьютеры. Оргтехника".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item11, context);

            var item12 = new ClassifierOrganization
            {
                Name = "Медицина и здоровье",
                Alias = "Медицина и здоровье".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item12, context);

            var item13 = new ClassifierOrganization
            {
                Name = "Услуги",
                Alias = "Услуги".ToAlias(),
                IsActive = true,
            };
            AddUpdateClassifierOrg(item13, context);

            context.SaveChanges();
        }

        private static void ReferencesInit(AppContext context)
        {
            var newsTags = new ApplicationReferences
            {
                Alias = AppConstants._NewsTags,
                Name = AppConstants._NewsTags,
                IsParent = true,
                Value = AppConstants._NewsTags,
                Key = AppConstants._NewsTags,
                IsActive = true
            };

            context.ApplicationReferences.Add(newsTags);

            context.SaveChanges();

        }

        private static void AddUpdateClassifierOrg(ClassifierOrganization entity,AppContext context)
        {
            if (entity!= null)
            {
                var editEntity = context.ClassifierOrganization.FirstOrDefault(x => x.Name == entity.Name);
                if (editEntity == null)
                {
                    context.ClassifierOrganization.Add(entity);
                    context.SaveChanges();
                }
            }
        }
    }
}
