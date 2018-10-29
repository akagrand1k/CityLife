using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.ReferencesService
{
    public interface IReferencesService
    {
        IEnumerable<ApplicationReferences> GetAll { get; }
        ApplicationReferences GetById(int id);
        ApplicationReferences Edit(ApplicationReferences entity);
        bool Delete(int Id);
    }
}
