using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.Entities.Models.Sections;
using CityLife.DataAccess.Interfaces;
using System.Linq.Expressions;
using CityLife.Extensions.ExtensionMethods;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.Extensions.Constant;

namespace CityLife.BusinessLogic.SectionServices
{
    public class SectionService : ISectionService
    {
        private const string appSection = "Разделы";
        private readonly IRepository<Section> repository;
        private readonly IUnitOfWork unitOfWork;
        private IEnumerable<Section> srcSections;

        private DateTime Today { get { return DateTime.Now; } }

        public SectionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new NullReferenceException("parameter: unitOfWork");

            //  this.repository = unitOfWork.Repository<Section>();

        }

        public IExceptionService ExceptionService
        {
            get
            {
                return new ExceptionService(unitOfWork);
            }
        }


        public IEnumerable<Section> Sections
        {
            get
            {
                try
                {
                    var ctx = unitOfWork.CityLifeDbContext;

                    var sections = new List<Section>();
                    sections = ctx.Set<Section>().ToList();
                    return sections;
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, appSection, SysConstants.AppException_Section);
                    throw new Exception(ex.Message);
                }
            }
        }

        public Section Edit(Section entry)
        {
            try
            {
                Section entity = new Section();

                var ctx = unitOfWork.CityLifeDbContext;

                var set = ctx
                    .Set<Section>();

                if (entry.Id > 0)
                {
                    entity = set
                        .FirstOrDefault(x => x.Id == entry.Id);
                }
                else
                {
                    set.Add(entity);
                }

                entity.Alias = entry.Name.ToAlias();
                entity.DateUpdate = Today;
                entity.IsActive = entry.IsActive;
                entity.IsDelete = entry.IsDelete;
                entity.Name = entry.Name;
                entity.Url = entry.Url;
                entity.ObsoleteUrl = entry.ObsoleteUrl;
                entity.Weight = entry.Weight;

                ctx.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, appSection, SysConstants.AppException_Section);
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Section> Get(Func<Section, bool> predicate)
        {
            IEnumerable<Section> sections = null;

            try
            {
                var ctx = unitOfWork.CityLifeDbContext;

                sections = ctx.Set<Section>()
                    .Where(predicate)
                    .ToList();

                return sections;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, appSection, SysConstants.AppException_Section);
                throw new Exception(ex.Message);
            }
        }

        public Section Get(int id)
        {
            try
            {
                Section entry = null;

                if (id > 0)
                {
                    var ctx = unitOfWork.CityLifeDbContext;
                    entry = ctx.Set<Section>()
                        .FirstOrDefault(x => x.Id == id);
                }

                return entry;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, appSection, SysConstants.AppException_Section);
                throw new Exception(ex.Message);
            }
        }

        public Section Get(string alias)
        {
            try
            {
                Section entry = null;

                if (!string.IsNullOrEmpty(alias))
                {
                    var ctx = unitOfWork.CityLifeDbContext;

                    entry = ctx.Set<Section>()
                        .FirstOrDefault(x => x.Alias == alias);
                }

                return entry;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, appSection, SysConstants.AppException_Section);
                throw new Exception(ex.Message);
            }
        }


        public bool Remove(int id)
        {
            try
            {
                Section section = null;
                var ctx = unitOfWork.CityLifeDbContext;
                if (id > 0)
                {

                    section = ctx.Set<Section>()
                        .FirstOrDefault(x => x.Id == id);
                }
                else
                {
                    throw new Exception("id required!");
                }

                section.IsDelete = true;
                section.IsActive = false;

                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, appSection, SysConstants.AppException_Section);
                throw new Exception(ex.Message);
            }
        }
    }
}
