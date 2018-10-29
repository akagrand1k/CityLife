using CityLife.BusinessLogic.AppExceptionService;
using CityLife.DALImplementation.UOW;
using CityLife.DataAccess.Interfaces;
using CityLife.DataAccess.Interfaces.UnitOfWork;
using CityLife.Entities.Models.News;
using CityLife.Entities.Models.References;
using CityLife.Extensions.Constant;
using CityLife.Extensions.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace CityLife.BusinessLogic.CityNewsService
{
    public class NewsService : INewsService
    {
        private readonly string section = "Новости";
        private readonly IRepository<Article> newsRepos;
        private readonly IRepository<ApplicationReferences> referencesRepos;
        private readonly IRepository<PreviewPictures> picRepos;
        private readonly IUnitOfWork unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter unitOfWork");

            //if (referencesRepos == null)
            //    throw new NullReferenceException("parameter referencesRepos");

            //if (picRepos == null)
            //    throw new NullReferenceException("parameter picRepos");

            this.unitOfWork = unitOfWork;
            newsRepos = unitOfWork.Repository<Article>();
            referencesRepos = unitOfWork.Repository<ApplicationReferences>();
            picRepos = unitOfWork.Repository<PreviewPictures>();

        }

        public IExceptionService ExceptionService
        {
            get
            {
                return new ExceptionService(unitOfWork);
            }
        }


        public IEnumerable<ApplicationReferences> Tags
        {
            get
            {
                try
                {
                    IEnumerable<ApplicationReferences> source = new List<ApplicationReferences>();
                    var ctx = unitOfWork.CityLifeDbContext;

                    
                    ApplicationReferences parent = ctx.Set<ApplicationReferences>()
                        .FirstOrDefault(x => x.Key == AppConstants._NewsTags);

                    if (parent == null)
                    {
                        return null;
                    }

                    source = ctx.Set<ApplicationReferences>()
                        .Where(x => x.ParentId == parent.Id && !x.IsDelete)
                        .ToList();

                    return source;
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable<Article> News
        {
            get
            {
                try
                {
                    IEnumerable<Article> source = new List<Article>();
                    var ctx = unitOfWork.CityLifeDbContext;

                    source = ctx.Set<Article>()
                        .Include(x => x.Tags)
                        .Include(x => x.PreviewPictures)
                        .Include(x => x.Owner)
                        .ToList();

                    return source;
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                    throw new Exception(ex.Message);
                }
            }
        }


        public IEnumerable<Article> Get(string tagAlias)
        {
            IEnumerable<Article> news = null;
            try
            {
                var ctx = unitOfWork.CityLifeDbContext;

                if (string.IsNullOrEmpty(tagAlias))
                {
                    news = ctx.Set<Article>()
                        .Include(x => x.Tags)
                        .Include(x => x.PreviewPictures)
                        .Include(x => x.Owner)
                        .ToList();
                }
                else
                {
                    ApplicationReferences tag = ctx
                        .Set<ApplicationReferences>()
                        .FirstOrDefault(x => x.Alias.ToLower() == tagAlias);

                    if (tag == null)
                    {
                        return null;
                    }

                    news = ctx.Set<Article>()
                        .Include(x => x.Tags)
                        .Include(x => x.PreviewPictures)
                        .Include(x => x.Owner)
                        .Where(x => x.Tags.Any(y => y.Id == tag.Id))
                        .ToList();
                }
                return news;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Article> Get(Func<Article, bool> predicate)
        {
            try
            {
                var ctx = unitOfWork.CityLifeDbContext;

                IEnumerable<Article> news = ctx.Set<Article>()
                    .Include(x => x.Tags)
                    .Include(x => x.PreviewPictures)
                    .Include(x => x.Owner)
                    .ToList();

                return news;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public Article Get(int id)
        {
            try
            {
                var ctx = unitOfWork.CityLifeDbContext;

                Article article = ctx.Set<Article>()
                    .Include(x => x.Tags)
                    .Include(x => x.PreviewPictures)
                    .Include(x => x.Owner)
                    .FirstOrDefault(x => x.Id == id);

                return article;

            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public Article Edit(Article entry, int[] tagIdentifiers)
        {
            try
            {
                Article entity = new Article();

                using (var ctx = unitOfWork.CityLifeDbContext)
                {
                    var artSet = ctx.Set<Article>();

                    if (entry.Id > 0)
                        entity = artSet
                            .Include(x => x.Tags)
                            .Include(x => x.PreviewPictures)
                            .Include(x=>x.Owner)
                            .FirstOrDefault(x => x.Id == entry.Id);
                    else
                        artSet.Add(entry);


                    entity.MetaKeywords = entry.MetaKeywords;
                    entity.MetaDescription = entry.MetaDescription;

                    entity.Name = entry.Name;
                    entity.Alias = entry.Name.ToAlias();

                    entry.DateUpdate = DateTime.Now;
                    entity.Content = entry.Content;
                    entity.ShortDescription = entry.ShortDescription;
                    entity.OwnerId = entity.OwnerId;

                    entity.IsActive = entry.IsActive;
                    entity.IsDelete = entry.IsDelete;

                    entity.PathPreviewPic = entry.PathPreviewPic;
                    entity.PathTitlePic = entry.PathTitlePic;
               

                    entity.Tags.Clear();

                    foreach (int tagId in tagIdentifiers)
                    {

                        ApplicationReferences tag = ctx.Set<ApplicationReferences>()
                            .FirstOrDefault(x => x.Id == tagId);
                        entity.Tags.Add(tag);
                    }
                  
                    ctx.SaveChanges();
                }

                return entity;

            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PreviewPictures> GetPictures
        {
            get
            {
                try
                {
                    return picRepos.GetAll
                        .ToList();
                }
                catch (Exception ex)
                {
                    ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                    throw new Exception(ex.Message);
                }
            }
        }

        public PreviewPictures GetPicture(int Id)
        {
            try
            {
                PreviewPictures picture = picRepos.Get(Id);
                return picture;

            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }


        public PreviewPictures SavePicture(int articleId, string smSizepath, string laSizePath, string alt)
        {
            try
            {
                PreviewPictures entity = new PreviewPictures();

                picRepos.Add(entity);

                entity.IsActive = true;
                entity.LargeSizePath = laSizePath;
                entity.SmallSizePath = smSizepath;
                entity.ArticleId = articleId;
                entity.Alt = alt;
                entity.Alias = alt.ToAlias();

                unitOfWork.Save();

                return entity;
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public void RemovePicture(int pictureId)
        {
            try
            {
                PreviewPictures entry = picRepos.Get(pictureId);
                picRepos.Delete(entry);
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public void RemovePicture(int articleId, int[] pictureIds = null)
        {
            try
            {
                IEnumerable<PreviewPictures> source = picRepos
                    .GetAll
                    .Where(x => x.ArticleId == articleId)
                    .ToList();

                if (pictureIds != null)
                {
                    source = source
                        .Where(x => pictureIds.Any(y => y == x.Id));
                }

                foreach (PreviewPictures item in source ?? new List<PreviewPictures>())
                {
                    picRepos.Delete(item);
                }
            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                newsRepos.Delete(id);

            }
            catch (Exception ex)
            {
                ExceptionService.WriteException(ex, section, SysConstants.AppException_CityNews);
                throw new Exception(ex.Message);
            }
        }
    }
}