using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.Entities.Models.Organizations;
using CityLife.DataAccess.Interfaces;
using CityLife.Extensions.ExtensionMethods;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.Extensions.Constant;

namespace CityLife.BusinessLogic.ClassifierService
{
    public class ClassifierService : IClassifierService
    {
        private const string section = "Организации";

        private IRepository<ClassifierOrganization> repository;

        private readonly IUnitOfWork unitOfWork;
        public ClassifierService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter: unitOfWork");
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<ClassifierOrganization>();
        }
        

        public IExceptionService ExceptionService
        {
            get
            {
                return new ExceptionService(unitOfWork);
            }
        }

        public IEnumerable<ClassifierOrganization> GetAll
        {
            get
            {
                try
                {
                    var result = repository.GetAll;
                    return result.Where(x => x.IsDelete == false)
                        .ToList();

                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_Classifier);
                    throw new Exception(ex.Message);
                }
            }
        }


        public bool Delete(int id)
        {
            try
            {
                var entity = repository.Get(id);
                repository.Update(entity);

                entity.IsDelete = true;
                unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Classifier);
                throw new Exception(ex.Message);
            }
        }

        public ClassifierOrganization Edit(ClassifierOrganization entity)
        {
            try
            {
                var classifier = new ClassifierOrganization();

                if (entity.Id > 0)
                    classifier = repository.Get(entity.Id);
                else
                    repository.Add(classifier);

                classifier.Alias = entity.Name.ToAlias();
                classifier.Name = entity.Name;
                classifier.Description = entity.Description;
                classifier.IsActive = entity.IsActive;
                classifier.IsDelete = entity.IsDelete;

                unitOfWork.Save();

                return classifier;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Classifier);
                throw new Exception(ex.Message);
            }
        }

        public ClassifierOrganization GetById(int id)
        {
            try
            {
                ClassifierOrganization entity = null;

                if (id > 0)
                {
                    entity = repository.Get(id);
                }
                return entity;

            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Classifier);
                throw new Exception(ex.Message);
            }
        }
        public List<ClassifierOrganization> GetMatchNames(string name)
        {
            try
            {
                List<ClassifierOrganization> result = new List<ClassifierOrganization>();

                var query1 = repository.Get(x => x.Name.ToLower().Contains(name.ToLower()))
                    .ToList();

                result = query1;

                return result;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Classifier);
                throw new Exception(ex.Message);
            }
        }
    }
}
