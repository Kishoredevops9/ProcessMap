namespace EKS.ProcessMaps.DA.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// IPublishContentRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPublishContentRepository<T>
        where T : class
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T Add(T t);

        /// <summary>
        /// AddAsyn
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> AddAsyn(T t);

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// CountAsync
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// DeleteAsyn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsyn(T entity);

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteAll(ICollection<T> entity);

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// FindAll
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        /// <summary>
        /// FindAllNoTracking
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        ICollection<T> FindAllNoTracking(Expression<Func<T, bool>> match);

        /// <summary>
        /// FindAllAsync
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// FindByAsyn
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Query
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Query();

        /// <summary>
        /// Queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Queryable();

        /// <summary>
        /// GetAllAsyn
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsyn();

        /// <summary>
        /// GetAllIncluding
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Save
        /// </summary>
        void Save();

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        T Update(T t, object key);

        /// <summary>
        /// UpdateAsyn
        /// </summary>
        /// <param name="t"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> UpdateExAsyn(T t, object key);

        Task<int> UpdateAsyn(T t, object key);

        // Task<int> UpdateAllAsyn(IEnumerable<T> t);
    }
}
