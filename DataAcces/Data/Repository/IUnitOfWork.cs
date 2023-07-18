using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Repository
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; set; }
        IDepartmentRepository DepartmentRepository { get; set; }
        void Save();
    }
}
