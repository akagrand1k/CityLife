using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.Entities.Models.References;
using CityLife.DataAccess.Interfaces;
using CityLife.Extensions.ExtensionMethods;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.Extensions.Constant;
using System.Data.Entity;

namespace CityLife.BusinessLogic.ReferencesService
{
    public class ReferencesService : IReferencesService
    {
        private readonly string section = "Справочники";
        private readonly IRepository<ApplicationReferences> repository;
        private readonly IUnitOfWork unitOfWork;
        public ReferencesService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter: unitOfWork");
            this.unitOfWork = unitOfWork;
            this.repository = this.unitOfWork.Repository<ApplicationReferences>();
        }


        public IExceptionService ExceptionService
        {
            get
            {
                return new ExceptionService(unitOfWork);
            }
        }


        public IEnumerable<ApplicationReferences> GetAll
        {
            get
            {
                try
                {
                    var ctx = unitOfWork.CityLifeDbContext;

                    var result = ctx
                        .Set<ApplicationReferences>()
                        .ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_References);
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var ctx = unitOfWork
                    .CityLifeDbContext;

                var reference = ctx.Set<ApplicationReferences>()
                    .FirstOrDefault(x => x.Id == id);

                reference.IsDelete = true;
                reference.IsActive = false;

                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_References);
                throw new Exception(ex.Message);
            }
        }

        public ApplicationReferences Edit(ApplicationReferences entity)
        {
            try
            {
                var editEntity = new ApplicationReferences();

                if (entity != null)
                {
                    var ctx = unitOfWork
                   .CityLifeDbContext;

                    DbSet<ApplicationReferences> set = ctx.Set<ApplicationReferences>();

                    if (entity.Id == 0)
                    {
                        repository.Add(editEntity);
                    }
                    else
                    {
                        editEntity = set.FirstOrDefault(x => x.Id == editEntity.Id);
                    }

                    editEntity.IsParent = entity.IsParent;
                    editEntity.ParentId = entity.ParentId;
                    editEntity.Name = entity.Name;
                    editEntity.Alias = entity.Name.ToAlias();
                    editEntity.IsActive = entity.IsActive;

                    editEntity.Key = entity.Key;
                    editEntity.Value = entity.Value;

                    ctx.SaveChanges();
                }

                return editEntity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_References);
                throw new Exception(ex.Message);
            }
        }

        public ApplicationReferences GetById(int id)
        {
            try
            {
                ApplicationReferences entity = null;

                if (id != 0)
                {
                    var ctx = unitOfWork.CityLifeDbContext;

                    entity = ctx.Set<ApplicationReferences>()
                        .FirstOrDefault(x => x.Id == id);

                    return entity;
                }

                return entity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_References);
                throw new Exception(ex.Message);
            }
        }
    }
}
