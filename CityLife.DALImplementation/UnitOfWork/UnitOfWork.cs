using CityLife.DataAccess.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.DataAccess.Interfaces;
using CityLife.Entities.Models.User;
using System.Data.Entity.Core.Objects;
using Microsoft.AspNet.Identity.EntityFramework;
using CityLife.DALImplementation.Context;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace CityLife.DALImplementation.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppContext context;
        private readonly ObjectContext objectContext;

        public DbContext CityLifeDbContext { get { return GetContext(); } }

        private bool disposed = false;
        public UnitOfWork(AppContext context)
        {
            this.context = context ?? new AppContext();
           

            //this.objectContext = ((IObjectContextAdapter)context).ObjectContext;
            //if (objectContext.Connection.State != System.Data.ConnectionState.Open)
            //{
            //    objectContext.Connection.Open();
            //}
        }

        public IRepository<T> Repository<T>() where T : class, new()
        {
            return new Repository<T>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                //if (objectContext.Connection.State == System.Data.ConnectionState.Open)
                //{
                //    objectContext.Connection.Close();
                //}
            }
            disposed = true;
        }

        private AppContext GetContext()
        {
            return new AppContext();
        }

    }
}
