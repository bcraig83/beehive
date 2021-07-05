using System;
using System.Linq;
using System.Linq.Expressions;

namespace Beehive.Api.Core.Interfaces.DataAccess
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> expression);
    }
}