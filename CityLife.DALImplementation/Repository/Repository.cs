using CityLife.DALImplementation.Context;
using CityLife.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CityLife.DALImplementation
{
    public class Repository<TEntity> : IRepository<TEntity>
            where TEntity : class, new()
    {
        private readonly AppContext context;

        private readonly DbSet<TEntity> dbSet;

        private bool disposed;

        public Repository(AppContext context)
        {
            if (context == null) { throw new NullReferenceException("context"); }
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll
        {
            get
            {
                return dbSet.AsNoTracking();
            }
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicaterExpr)
        {
            return dbSet.AsNoTracking().Where(predicaterExpr);
        }

        public TEntity Get(int id)
        {
            TEntity entry = dbSet.Find(id);

            if (entry == null)
            {
                return null;
            }

            return entry;
        }

        public TEntity Get(string alias)
        {
            TEntity entry = null;

            try
            {
                entry = dbSet.Find(alias);
            }
            catch
            {
                return null;
            }

            if (entry == null) { return null; }

            return entry;
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = dbSet.AsNoTracking();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }

        public bool Delete(int id)
        {
            TEntity entry = null;

            try
            {
                entry = Get(id);
            }
            catch (Exception) { return false; }

            if (entry == null) { return false; }

            return Delete(entry);
        }

        public bool Delete(TEntity entity)
        {
            if (entity == null) { return false; }

            try
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }

                dbSet.Remove(entity);

            }
            catch { return false; }

            return true;
        }

        public bool Add(TEntity entity)
        {
            if (entity == null) { return false; }

            try
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Add(entity);

            }
            catch (Exception ex) { return false; }

            return true;
        }

        public bool Update(TEntity entity)
        {
            if (entity == null) { return false; }

            try
            {
                //if (context.Entry(entity).State == EntityState.Detached)
                //{
                //    dbSet.Attach(entity);
                //}

                context.Entry(entity).State = EntityState.Modified;

            }
            catch (Exception ex)
            { return false; }

            return true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
