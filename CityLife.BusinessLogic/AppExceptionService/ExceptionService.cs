using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.DataAccess.Interfaces;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.Entities.Models.Exceptions;
using System.Diagnostics;

namespace CityLife.BusinessLogic.AppExceptionService
{
    public class ExceptionService : IExceptionService
    {
        private IRepository<AppExpection> repository;
        private readonly IUnitOfWork unitOfWork;

        public ExceptionService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter: unitOfWork");
            this.unitOfWork = unitOfWork;

            //  repository = unitOfWork.Repository<AppExpection>();
        }

        public IEnumerable<AppExpection> GetAll
        {
            get
            {
                var ctx = unitOfWork.CityLifeDbContext;

                var result = ctx.Set<AppExpection>().ToList();

                return result;
            }
        }

        public void WriteException(Exception e, string section, string key)
        {
            var entity = new AppExpection();

            var ctx = unitOfWork.CityLifeDbContext;

            ctx.Set<AppExpection>().Add(entity);

            entity.ExceptionMessage = e.Message;
            entity.ExceptionType = e.GetType().Name;
            entity.ExceptionSource = new StackTrace(e).GetFrame(0).GetMethod().Name;
            entity.Log_Key = key;
            if (e.InnerException != null)
                entity.InnerExceptionMessage = e.InnerException.Message;
            entity.Section = section;

            ctx.SaveChanges();
        }
    }
}
