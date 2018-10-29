using CityLife.Entities.Models.News;
using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.CityNewsService
{
    public interface INewsService
    {
        /// <summary>
        /// Tag for news
        /// </summary>
        IEnumerable<ApplicationReferences> Tags { get; }
        /// <summary>
        /// All news
        /// </summary>
        IEnumerable<Article> News { get; }

        /// <summary>
        /// Select articles by specified predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Article> Get(Func<Article, bool> predicate);

        /// <summary>
        /// Select articles by tag alias
        /// </summary>
        /// <param name="tagAlias"></param>
        /// <returns></returns>
        IEnumerable<Article> Get(string tagAlias);

        /// <summary>
        /// Get article by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Article Get(int id);

        /// <summary>
        /// Edit article
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="tagIdentifiers"></param>
        /// <returns></returns>
        Article Edit(Article entry, int[] tagIdentifiers);

        /// <summary>
        /// Get collection pictures
        /// </summary>
        IEnumerable<PreviewPictures> GetPictures { get; }

        /// <summary>
        /// Get single picture by identifier
        /// </summary>
        /// <param name="Id">Picture identifier</param>
        /// <returns></returns>
        PreviewPictures GetPicture(int Id);

        /// <summary>
        /// Save preview picture.
        /// </summary>
        /// <param name="articleId">Article identifier </param>
        /// <param name="smSizepath">Small size path</param>
        /// <param name="laSizePath">Large size path</param>
        /// <param name="alt">Description image</param>
        /// <returns></returns>
        PreviewPictures SavePicture(int articleId, string smSizepath, string laSizePath, string alt);

        /// <summary>
        /// Remove single picture by identifier.
        /// </summary>        
        /// <param name="pictureId"></param>        
        void RemovePicture(int pictureId);

        /// <summary>
        /// Remove picture collection by article identifier.
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="pictureIds">Picture identifiers for delete</param>
        void RemovePicture(int articleId,int[]pictureIds=null);

        /// <summary>
        /// Delete single article
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
                
    }
}
