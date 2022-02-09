using System;
using System.Linq;
using System.Linq.Expressions;
using Beehive.Api.Core.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Beehive.Api.Infrastructure.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
        }

        public IQueryable<T> GetAll()
        {
            return Query(x => true);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> expression)
        {
            // Need to add code to handle "EntityNotFoundException"
            return _dbSet.Where(expression);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}