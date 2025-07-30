using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Unit_Of_Work
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get;}
        public IEmployeeRepository EmployeeRepository { get; }

        int SaveChanges();
    }
}
