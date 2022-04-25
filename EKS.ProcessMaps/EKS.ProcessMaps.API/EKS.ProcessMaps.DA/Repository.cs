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
    using Microsoft.EntityFrameworkCore.Storage;

    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly KnowledgeMapContext _context;

        /// <summary>
        /// Constructor - Repository
        /// </summary>
        /// <param name="context"></param>
        public Repository(KnowledgeMapContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetAll - IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Query()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToList();
        }

        /// <summary>
        /// GetAllAsyn
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> GetAllAsyn()
        {

            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual T Add(T t)
        {

            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        /// <summary>
        /// AddAsyn
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsyn(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;

        }

        public virtual int AddRange(ICollection<T> entity)
        {
            _context.Set<T>().AddRange(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// FindAll
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        /// <summary>
        /// FindAllNoTracking
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public ICollection<T> FindAllNoTracking(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().AsNoTracking().Where(match).ToList();
        }

        /// <summary>
        /// FindAllAsync
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().AsNoTracking().Where(match).ToListAsync();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// DeleteAsyn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsyn(T entity)
        {           
            _context.Set<T>().RemoveRange(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// DeleteAll Synchrounusly
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int DeleteAll(ICollection<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            return _context.SaveChanges();
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

            T exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.Set<T>().Update(t);
                _context.SaveChanges();
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

            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public virtual async Task<int> UpdateAsyn(T t, object key)
        {
            if (t == null)
            {
                return 0;
            }

            _context.Set<T>().Update(t);
            return await _context.SaveChangesAsync();
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
            return _context.Set<T>().Count();
        }

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        /// <summary>
        /// Save
        /// </summary>
        public virtual void Save()
        {

            _context.SaveChanges();
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        /// <summary>
        /// FindByAsyn
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// GetAllIncluding
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }
        /// <summary>
        /// BeginTransaction
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return this._context.Database.BeginTransaction();
        }

        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
