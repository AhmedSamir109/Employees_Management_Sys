using EmpsManagement.DAL.Data.Contexts;
using EmpsManagement.DAL.Models.Departments;
using EmpsManagement.DAL.Repositories.Interfaces;

namespace EmpsManagement.DAL.Repositories.Classes
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {

        public DepartmentRepository(ApplicationDbContext dbContect): base(dbContect)
        {

        }


        #region nonGeneric Sol
        
        //GetAll
        //public IEnumerable<Department> GetAll(bool WithTracking = false)
        //{
        //    if (WithTracking)
        //    {
        //        return _dbContect.Departments.ToList();
        //    }
        //    return _dbContect.Departments.AsNoTracking().ToList();
        //}


        ////GetById
        //public Department? GetById(int id) => _dbContect.Departments.Find(id);         // (=>) fat Arrow Or goes To


        ////insert
        //public int Add(Department department)
        //{
        //    _dbContect.Departments.Add(department);
        //    return _dbContect.SaveChanges();
        //}


        ////Update
        //public int Update(Department department)
        //{
        //    _dbContect.Departments.Update(department);
        //    return _dbContect.SaveChanges();
        //}

        //// Delete 
        //public int Remove(Department department)
        //{
        //    _dbContect.Departments.Remove(department);
        //    return _dbContect.SaveChanges();
        //}

        #endregion

    }
}
