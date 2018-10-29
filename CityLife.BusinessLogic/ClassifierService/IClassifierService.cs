using CityLife.Entities.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.ClassifierService
{
    public interface IClassifierService
    {
        IEnumerable<ClassifierOrganization> GetAll { get; }
        ClassifierOrganization GetById(int id);
        ClassifierOrganization Edit(ClassifierOrganization entity);
        bool Delete(int id);
        List<ClassifierOrganization> GetMatchNames(string name);
    }
}
