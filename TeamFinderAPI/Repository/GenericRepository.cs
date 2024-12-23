using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamFinderAPI.Data;

namespace TeamFinderAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly TeamFindAPIContext _context;

        public GenericRepository(TeamFindAPIContext context){
            _context = context;
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            
        }

        public T FindBy (Expression<Func<T,bool>> predicate){
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T> ().AddRange(entities);
        }

        public IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T> ().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}