namespace EKS.ProcessMaps.DA
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.DA.Interfaces;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// PublishContentRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PublishContentRepository<T> : IPublishContentRepository<T>
        where T : class
    {
        private readonly PublishContentContext _context;

        /// <summary>
        /// Constructor - PublishContentRepository
        /// </summary>
        /// <param name="context"></param>
        public PublishContentRepository(PublishContentContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// GetAll - IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return this._context.Set<T>();
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Query()
        {
            IQueryable<T> query = this._context.Set<T>();
            return query.ToList();
        }

        /// <summary>
        /// Queryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Queryable()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        /// <summary>
        /// GetAllAsyn
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await this._context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return this._context.Set<T>().Find(id);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(int id)
        {
            return await this._context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual T Add(T t)
        {
            this._context.Set<T>().Add(t);
            this._context.SaveChanges();
            return t;
        }

        /// <summary>
        /// AddAsyn
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsyn(T t)
        {
            this._context.Set<T>().Add(t);
            await this._context.SaveChangesAsync();
            return t;
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return this._context.Set<T>().SingleOrDefault(match);
        }

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await this._context.Set<T>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// FindAll
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return this._context.Set<T>().Where(match).ToList();
        }

        /// <summary>
        /// FindAllNoTracking
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public ICollection<T> FindAllNoTracking(Expression<Func<T, bool>> match)
        {
            return this._context.Set<T>().AsNoTracking().Where(match).ToList();
        }

        /// <summary>
        /// FindAllAsync
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await this._context.Set<T>().AsNoTracking().Where(match).ToListAsync();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
            this._context.SaveChanges();
        }

        /// <summary>
        /// DeleteAsyn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsyn(T entity)
        {
            this._context.Set<T>().RemoveRange(entity);
            return await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// DeleteAll Synchrounusly
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int DeleteAll(ICollection<T> entity)
        {
            this._context.Set<T>().RemoveRange(entity);
            return this._context.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Update(T t, object key)
        {
            if (t == null)
            {
                return null;
            }

            T exist = this._context.Set<T>().Find(key);
            if (exist != null)
            {
                this._context.Entry(exist).CurrentValues.SetValues(t);
                this._context.Set<T>().Update(t);
                this._context.SaveChanges();
            }

            return exist;
        }

        /// <summary>
        /// UpdateAsyn
        /// </summary>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> UpdateExAsyn(T t, object key)
        {
            if (t == null)
            {
                return null;
            }

            T exist = await this._context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                this._context.Entry(exist).CurrentValues.SetValues(t);
                await this._context.SaveChangesAsync();
            }

            return exist;
        }

        public virtual async Task<int> UpdateAsyn(T t, object key)
        {
            if (t == null)
            {
                return 0;
            }

            this._context.Set<T>().Update(t);
            return await this._context.SaveChangesAsync();
        }

        /*
        public virtual async Task<int> UpdateAllAsyn(IEnumerable<T> t)
        {
            if (t == null)
            {
                return 0;
            }

            _context.Set<T>().UpdateRange(t);
            return await _context.SaveChangesAsync();
        }*/

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this._context.Set<T>().Count();
        }

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await this._context.Set<T>().CountAsync();
        }

        /// <summary>
        /// Save
        /// </summary>
        public virtual void Save()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this._context.Set<T>().Where(predicate);
            return query;
        }

        /// <summary>
        /// FindByAsyn
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await this._context.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// GetAllIncluding
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = this.GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
