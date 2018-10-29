using CityLife.Entities.Models.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.SectionServices
{
    public interface ISectionService
    {
        IEnumerable<Section> Sections { get; }
        IEnumerable<Section> Get(Func<Section, bool> predicate);
        Section Get(int id);
        Section Get(string alias);       
        Section Edit(Section entry);
        bool Remove(int id);
    }
}
