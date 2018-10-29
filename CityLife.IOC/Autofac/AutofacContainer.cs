using Autofac;
using Autofac.Integration.Mvc;
using CityLife.Authenticate.Extends.Provider;
using CityLife.Authenticate.Options;
using CityLife.BusinessLogic.ActionLogService;
using CityLife.BusinessLogic.Agregate;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.BusinessLogic.CityNewsService;
using CityLife.BusinessLogic.ClassifierService;
using CityLife.BusinessLogic.OrganizationService;
using CityLife.BusinessLogic.ReferencesService;
using CityLife.BusinessLogic.SectionServices;
using CityLife.BusinessLogic.SysReferencesService;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.DALImplementation;
using CityLife.DALImplementation.Context;
using CityLife.DALImplementation.UOW;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.Entities.Models.News;
using CityLife.Entities.Models.Organizations;
using CityLife.Entities.Models.References;
using CityLife.Entities.Models.Sections;
using CityLife.Entities.Models.User;
using System.Reflection;
using System.Web.Mvc;

namespace CityLife.IoC.Autofac
{
    public class AutofacContainer
    {
        const string parameterName = "repository";
        public static void GetContainer(Assembly assembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(assembly);

            //EF unit of work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
                
            builder.RegisterType<SysReferencesService>().As<ISysReferencesService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<InitialPrimaryData>().As<IInitialPrimaryData>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<ReferencesService>().As<IReferencesService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<UserProfileService>().As<IUserProfileService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<UserInfoProvider>().As<IAuthUserInfo<UserProfile>>();

            builder.RegisterType<ClassifierService>().As<IClassifierService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<OrganizationService>().As<IOrganizationService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<SectionService>().As<ISectionService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<NewsService>().As<INewsService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<ExceptionService>().As<IExceptionService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));

            builder.RegisterType<ActionLogService>().As<IActionLogService>()
                .WithParameter("unitOfWork", new UnitOfWork(new AppContext()));


            //Example of using more than one parameter

            //builder.RegisterType<InitialPrimaryData>().As<IInitialPrimaryData>()
            //    .WithParameter(parameterName, new Repository<EntityFirst>(new AppContext()))
            //    .WithParameter(parameterName, new Repository<EntitySecond>(new AppContext()))
            //    .WithParameter(parameterName, new Repository<EntityMore>(new AppContext()));


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }


       
    }
}