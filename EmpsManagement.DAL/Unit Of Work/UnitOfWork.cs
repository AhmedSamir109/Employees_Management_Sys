using EmpsManagement.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IDepartmentRepository>_departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _departmentRepository =   new Lazy<IDepartmentRepository>( () => new DepartmentRepository(dbContext) );
            _employeeRepository = new Lazy<IEmployeeRepository>( () => new EmployeeRepository(dbContext));

        }
        public IDepartmentRepository DepartmentRepository { get { return _departmentRepository.Value; } }

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
