using Microsoft.EntityFrameworkCore;
using Project.App.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.BulkOperations;

namespace Project.App.DesignPatterns.Reponsitories
{
    public partial interface IRepositoryBaseMariaDB<T> 
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(List<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        bool Any(Expression<Func<T, bool>> expression);
        bool All(Expression<Func<T, bool>> expression);
        Task BulkAsync(IEnumerable<T> entities);
    }

    public partial class RepositoryBaseMariaDB<T> : IRepositoryBaseMariaDB<T> where T : class
    {
        private readonly MariaDBContext DbContext;
        public RepositoryBaseMariaDB(MariaDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> FindAll()
        {
            return DbContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Where(expression);
        }

        public void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }
        
        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }
        public bool Any(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().Any(expression);
        }

        public async Task BulkAsync(IEnumerable<T> entities)
        {
            await DbContext.Set<T>().BulkInsertAsync(entities);
        }

        public bool All(Expression<Func<T, bool>> expression)
        {
            return DbContext.Set<T>().All(expression);
        }
    }
}
