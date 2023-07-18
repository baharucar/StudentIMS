using DataAccess.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private StudentDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(StudentDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query=query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Add(T obj)
        {
            _dbSet.Add(obj);
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public void DeleteRange(IEnumerable<T> obj)
        {
            _dbSet.RemoveRange(obj);
        }       

        public IEnumerable<T> GetAll(string? properties=null)
        {
            IQueryable<T> query = _dbSet;

            if(properties is not null)
            {
                foreach (var property in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)){
                    query = query.Include(property);
                }

            }

            return query.ToList();
        }
    }
}
