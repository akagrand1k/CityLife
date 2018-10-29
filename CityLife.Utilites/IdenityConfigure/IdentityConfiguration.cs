using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using CityLife.BusinessLogic.Interfaces;
namespace CityLife.Utilites.IdenityConfigure
{
    public class IdentityConfiguration : IIdentityConfiguration
    {
        private IProviderService provider;
        public IdentityConfiguration(IProviderService provider)
        {
            this.provider = provider;
        }
        public void ConfigureAuthenticate(IAppBuilder app)
        {
            app.CreatePerOwinContext(provider.GetContext);
        }
    }
}
