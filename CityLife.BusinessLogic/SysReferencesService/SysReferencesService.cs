using CityLife.DataAccess.Interfaces;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityLife.BusinessLogic.SysReferencesService
{
    public class SysReferencesService : ISysReferencesService
    {
        private readonly IRepository<ApplicationSysReferences> repository;
        private readonly IUnitOfWork unitOfWork;
        public SysReferencesService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter: unitOfWork");
            this.unitOfWork = unitOfWork;
            //  repository = unitOfWork.Repository<ApplicationSysReferences>();
        }

        public IEnumerable<ApplicationSysReferences> GetAll
        {
            get
            {
                IEnumerable<ApplicationSysReferences> result = new List<ApplicationSysReferences>();

                var ctx = unitOfWork.CityLifeDbContext;

                result = ctx
                    .Set<ApplicationSysReferences>()
                    .ToList();

                return result;
            }
        }

        public void CreateSysByKey(string key, string value)
        {
            var entity = new ApplicationSysReferences();
            List<ApplicationSysReferences> allReferences = new List<ApplicationSysReferences>();

            var ctx = unitOfWork.CityLifeDbContext;

            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                var result = ctx
                    .Set<ApplicationSysReferences>()
                    .FirstOrDefault(x => x.Key == key);

                if (result == null)
                {
                    entity.Key = key;
                    entity.Value = value;
                    ctx.Set<ApplicationSysReferences>().Add(entity);
                    ctx.SaveChanges();
                }
            }
        }

        public void Edit(ApplicationSysReferences entity)
        {
            if (entity != null)
            {
                var ctx = unitOfWork.CityLifeDbContext;

                var editEntity = ctx
                     .Set<ApplicationSysReferences>()
                     .FirstOrDefault(x => x.Key == entity.Key);

                editEntity.Value = entity.Value;

                ctx.SaveChanges();
            }
        }

        public ApplicationSysReferences GetSysRefById(int id)
        {
            ApplicationSysReferences entity = null;

            if (id > 0)// -1
            {
                var ctx = unitOfWork.CityLifeDbContext;
                entity = ctx
                     .Set<ApplicationSysReferences>()
                     .FirstOrDefault(x => x.Id == id);
                return entity;
            }

            return entity;
        }

        public string GetValueByKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {

                var ctx = unitOfWork.CityLifeDbContext;
                var entity = ctx
                     .Set<ApplicationSysReferences>()
                     .FirstOrDefault(x => x.Key == key);

                if (entity != null)
                {
                    return entity.Value;
                }

                return null;

            }

            return string.Empty;
        }
    }
}
