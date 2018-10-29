using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.Entities.Models.Organizations;
using CityLife.DataAccess.Interfaces;
using CityLife.Extensions.ExtensionMethods;
using CityLife.Extensions.Constant;
using System.Linq.Expressions;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.Entities.Models.Custom;

namespace CityLife.BusinessLogic.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private readonly string section = "Организации";

        private IRepository<Organization> orgrepository;
        private IRepository<ClassifierOrganization> classifierRep;

        private readonly IUnitOfWork unitOfWork;
        public OrganizationService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter: unitOfWork");

            this.unitOfWork = unitOfWork;
            orgrepository = unitOfWork.Repository<Organization>();
            classifierRep = unitOfWork.Repository<ClassifierOrganization>();
        }

        public IExceptionService ExceptionService
        {
            get
            {
                return new ExceptionService(unitOfWork);
            }
        }


        public IEnumerable<Organization> GetAll
        {
            get
            {
                try
                {
                    var context = unitOfWork.CityLifeDbContext;

                    var result = context.Set<Organization>().ToList();
                    //var result = orgrepository.GetAll
                    //    .ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_Organization);
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<YmapJSONData> GetGeoObject(string query, string critery)
        {
            List<YmapJSONData> result = new List<YmapJSONData>();
            var context = unitOfWork.CityLifeDbContext;

            if (query == "")
            {
                //  var query1 = orgrepository.GetAll;


                var query1 = context.Set<Organization>().ToList();

                var query2 = query1.Select(z => new YmapJSONData
                {
                    Features = new Features
                    {
                        id = z.Id,

                        Geometry = new Geometry
                        {
                            //Coordinates = new List<string>().Add(z.Latitude.ToString())
                        },

                        properties = new Properties
                        {
                            balloonContentHeader = "<font size=3><b><a target='_blank' href='#'>" + z.Name + "</a></b></font>",
                            balloonContentBody = z.ShortDescription,
                            clusterCaption = "<strong>" + z.Name + "</strong>",
                            //hintContent = $""
                        },
                    },

                });
                result = query2.ToList();
            }

            if (query == "cl")
            {
                var classifier = context.Set<ClassifierOrganization>().FirstOrDefault(z => z.Alias == critery);
                //                var classifier = classifierRep.Get(critery);

                if (classifier != null)
                {
                    //var organizations = orgrepository.GetAll.Where(x => x.ClassifierId == classifier.Id);
                    var organizations = context.Set<Organization>().Where(z => z.ClassifierId == classifier.Id).ToList();

                    var query2 = organizations.Select(z => new YmapJSONData
                    {
                        Features = new Features
                        {
                            id = z.Id,

                            Geometry = new Geometry
                            {
                                //       Coordinates = new decimal[] { z.Latitude, z.Longitude },
                            },

                            properties = new Properties
                            {
                                balloonContentHeader = "<font size=3><b><a target='_blank' href='#'>" + z.Name + "</a></b></font>",
                                balloonContentBody = z.ShortDescription,
                                clusterCaption = $"<strong>" + z.Name + "</strong>",
                                //hintContent = $""
                            },
                        },

                    });
                    result = query2.ToList();
                }
            }
            if (query == "org")
            {
                Organization org = null;
                int intValCritery = 0;
                if (int.TryParse(critery, out intValCritery))
                {
                    if (intValCritery > 0)
                    {
                        org = context.Set<Organization>().FirstOrDefault(z => z.Id == intValCritery);
                    }
                }

                //var organization = orgrepository.Get(int.Parse(critery));
                if (org != null)
                {
                    var val2 = new YmapJSONData
                    {
                        Features = new Features
                        {
                            id = org.Id,
                            Geometry = new Geometry
                            {
                                //        Coordinates = new decimal[] { organization.Latitude, organization.Longitude },
                            },

                            properties = new Properties
                            {
                                balloonContentHeader = "<font size=3><b><a target='_blank' href='#'>" + org.Name + "</a></b></font",
                                balloonContentBody = org.ShortDescription,
                                clusterCaption = $"<strong>" + org.Name + "</strong>",
                                //hintContent = $""
                            },
                        },

                    };

                    result.Add(val2);
                }
            }

            return result;
        }

        public IQueryable<Organization> GetWithInclude(params Expression<Func<Organization, object>>[] includeExpressions)
        {
            try
            {
                return orgrepository.Include(includeExpressions);
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Organization);
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = orgrepository.Get(id);
                entity.IsDelete = true;
                entity.IsActive = false;
                orgrepository.Update(entity);

                unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Organization);
                throw new Exception(ex.Message);
            }

        }

        public Organization Edit(Organization entity)
        {
            try
            {
                var context = unitOfWork.CityLifeDbContext;

                Organization org = new Organization();

                if (entity.Id > 0)
                    org = context.Set<Organization>().FirstOrDefault(z => z.Id == entity.Id);
                else
                    context.Set<Organization>().Add(org);

                org.HeightImg = entity.HeightImg;
                org.WidthImg = entity.WidthImg;

                if (entity.LargePathImage != null)
                    org.LargePathImage = entity.LargePathImage;

                if (entity.SmallPathImage != null)
                    org.SmallPathImage = entity.SmallPathImage;

                org.ShortDescription = entity.ShortDescription;

                org.OwnerId = entity.OwnerId;
                org.ClassifierId = entity.ClassifierId;

                org.Alias = entity.Name.ToAlias();
                org.Name = entity.Name;
                org.DetailsDescription = entity.DetailsDescription;
                org.IsActive = entity.IsActive;
                org.IsDelete = entity.IsDelete;
                org.Building = entity.Building;
                org.Latitude = entity.Latitude;
                org.Longitude = entity.Longitude;
                org.Street = entity.Street;
                org.Notation = entity.Notation;
                org.Home = entity.Home;
                org.IsActive = true;

                context.SaveChanges();
                //unitOfWork.Save();

                return org;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Organization);
                throw new Exception(ex.Message);
            }
        }

        public Organization GetById(int id)
        {
            try
            {
                var context = unitOfWork.CityLifeDbContext;
                Organization entity = null;

                if (id > 0)
                {
                    //entity = orgrepository.Get(id);
                    entity = context.Set<Organization>().FirstOrDefault(z => z.Id == id);
                }

                return entity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Organization);
                throw new Exception(ex.Message);
            }
        }

        public List<Organization> GetMatchNames(string name)
        {
            try
            {
                var context = unitOfWork.CityLifeDbContext;

                List<Organization> result = new List<Organization>();

                var query1 = context.Set<Organization>().Where(z => z.Name.ToLower().Contains(name.ToLower())).ToList();

                //                var query1 = orgrepository.Get(x => x.Name.ToLower().Contains(name.ToLower()))
                //                  .ToList();

                result = query1;

                return result;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_Organization);
                throw new Exception(ex.Message);
            }
        }

        public Organization GetById(string id)
        {
            Organization org = null;
            var context = unitOfWork.CityLifeDbContext;



            if (!string.IsNullOrEmpty(id))
            {
                org = context.Set<Organization>().FirstOrDefault(z => z.OwnerId == id);
                //    org = orgrepository.Include().FirstOrDefault(x => x.Owner.Id == id);
            }
            return org;

        }
    }
}
