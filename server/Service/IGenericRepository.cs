using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TeamFinderAPI.Service
{
    public interface IGenericRepository<T> where T : class 
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T,bool>> predicate);
        void Add (T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove (T entity);
        void RemoveRange (IEnumerable<T> entities);
    }
}