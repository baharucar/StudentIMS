using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? properties);
        T Get(Expression<Func<T,bool>> filter);
        void Add(T obj);
        void Delete(T obj);
        void DeleteRange(IEnumerable<T> obj);
    }


}
