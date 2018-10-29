using CityLife.DALImplementation.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;


namespace CityLife.Authenticate.Managers
{
    public class AppRoleManager:RoleManager<IdentityRole>
    {
        public AppRoleManager(RoleStore<IdentityRole> store):base(store)
        {

        }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var dbContext = new AppContext();
            var roleManager =
                new AppRoleManager(new RoleStore<IdentityRole>(dbContext));

            return roleManager;
        }
    }
}
