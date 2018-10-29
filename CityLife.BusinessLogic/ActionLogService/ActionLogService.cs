using CityLife.DataAccess.Interfaces;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.ActionLogService
{
    public class ActionLogService : IActionLogService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<ActionLog> repository;
        public IEnumerable<ActionLog> Logs => throw new NotImplementedException();

        public ActionLogService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException();

            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<ActionLog>();

        }
        public ActionLog Get(int id)
        {
            if (id == 0)
            {
                return null;
            }

            ActionLog entry = repository.Get(id);

            return entry;

        }

        public ActionLog Get(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                return null;
            }

            ActionLog entry = repository.GetAll.FirstOrDefault(x => x.Alias == alias);

            return entry;
        }

        public ActionLog Get(DateTime concreteDate)
        {
            throw new NotImplementedException();
        }

        public ActionLog Get(DateTime since, DateTime till)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActionLog> Get(Func<ActionLog, bool> predicate)
        {
            return repository.GetAll
                .Where(predicate)
                .ToList();
        }


        public ActionLog Edit(ActionLog entry)
        {
            ActionLog entity = null;

            if (entry.Id > 0)
            {
                entity = repository.Get(entry.Id);

                if (entity != null)
                {
                    repository.Add(entity);
                }
                else
                {
                    throw new NullReferenceException("entry not found!");
                }
            }

            entity.Name = entry.Name;
            entity.Alias = entry.Alias;
            entity.Data = entry.Data;
            entity.DateCreate = entry.DateCreate;
            entity.DateUpdate = entry.DateUpdate;
            entity.EntityName = entry.EntityName;
            entity.InnerReferralUrl = entry.InnerReferralUrl;
            entity.IsActive = entry.IsActive;
            entity.IsDelete = entry.IsDelete;
            
            unitOfWork.Save();

            return entity;
        }

        public void Delete(int id)
        {
            repository.Delete(id);

            unitOfWork.Save();
        }
    }
}
