using DataAccess.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _db;
        public IStudentRepository StudentRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(StudentDbContext db)
        {
            _db = db;
            StudentRepository = new StudentRepository(_db);
            DepartmentRepository = new DepartmentRepository(_db);
        }        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
