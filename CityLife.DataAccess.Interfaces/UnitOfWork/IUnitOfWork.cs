using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.DataAccess.Interfaces.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {   
        DbContext CityLifeDbContext { get; }

        IRepository<T> Repository<T>() where T : class,new();
        
        void Save();

        void Rollback();

    }
}
