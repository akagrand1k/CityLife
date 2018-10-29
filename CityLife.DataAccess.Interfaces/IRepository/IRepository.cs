using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.DataAccess.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll { get; }
        
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicateExpr);
        /// <summary>
        /// Include navigations properties
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions);
        TEntity Get(int id);
        TEntity Get(string alias);
        bool Add(TEntity entry);
        bool Delete(TEntity entry);
        bool Delete(int id);
        bool Update(TEntity entry);

    }
}
