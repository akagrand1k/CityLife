using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.ActionLogService
{
    public interface IActionLogService
    {
        IEnumerable<ActionLog> Logs { get; }
        IEnumerable<ActionLog> Get(Func<ActionLog, bool> predicate);
        ActionLog Get(int id);
        ActionLog Get(string alias);
        ActionLog Get(DateTime concreteDate);
        ActionLog Get(DateTime since, DateTime till);
        ActionLog Edit(ActionLog entry);
        void Delete(int id);
    }
}
