using EmpsManagement.DAL.Data.Contexts;
using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Repositories.Classes
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
       
        public EmployeeRepository(ApplicationDbContext dbContext): base(dbContext)
        {

        }

        #region nonGeneric Sol

        //public IEnumerable<Employee> GetAll(bool WithTracking = false)
        //{
        //    if (WithTracking)
        //    {
        //        return _dbContext.Employees.ToList();
        //    }
        //    return _dbContext.Employees.AsNoTracking().ToList();

        //}
        //public Employee? GetById(int id)
        //{
        //    return _dbContext.Employees
        //        .Find(id);
        //}
        //public int Add(Employee employee)
        //{
        //    _dbContext.Add(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    _dbContext.Update(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Remove(Employee employee)
        //{
        //    _dbContext.Remove(employee);
        //    return _dbContext.SaveChanges();
        //} 

        #endregion

    }
}
