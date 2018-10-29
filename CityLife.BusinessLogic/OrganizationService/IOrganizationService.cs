using CityLife.Entities.Models.Custom;
using CityLife.Entities.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.OrganizationService
{
    public interface IOrganizationService
    {
        IEnumerable<Organization> GetAll { get; }
        List<YmapJSONData> GetGeoObject(string query,string critery);

        Organization GetById(int id);
        Organization GetById(string id);
        IQueryable<Organization> GetWithInclude(params Expression<Func<Organization, object>>[] includeExpressions);
        Organization Edit(Organization entity);
        bool Delete(int id);

        /// <summary>
        /// Find name organization and organization classifier by critery.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<Organization> GetMatchNames(string name);
    }
}
