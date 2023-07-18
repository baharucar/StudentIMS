using DataAccess.Data.Repository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class StudentRepository : Repository<Student>, IStudentRepository //Repository<StudentDBContext> 
    {
        private readonly StudentDbContext _db;

        public StudentRepository(StudentDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Student obj)
        {
            _db.Students.Update(obj);
        }      
    }
}
