using EmpsManagement.BLL.DTOs.Department;
using EmpsManagement.BLL.Factories;
using EmpsManagement.DAL.Repositories.Interfaces;
using EmpsManagement.DAL.Unit_Of_Work;
using System.Linq.Expressions;

namespace EmpsManagement.BLL.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetAll
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            return _unitOfWork.DepartmentRepository.GetAll(true).Select(d => d.ToDepartmentDto());

        }
        #endregion

        #region GetById
        public DepartmentDetailsDto? GetbyId(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            return department is null ? null : department.ToDepartmentDetailsDto();

        }

        #endregion

        #region Insert
        public int createDepartment(CreateDepartmentDto departmentDto)
        {
             _unitOfWork.DepartmentRepository.Add(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();  // SaveChanges() will commit the changes to the database and return the number of affected rows
        }

        #endregion

        #region Update
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity()) ;
            return _unitOfWork.SaveChanges();  // SaveChanges() will commit the changes to the database and return the number of affected rows
        } 

        #endregion

        #region Delete
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            _unitOfWork.DepartmentRepository.Remove(department) ;
            return _unitOfWork.SaveChanges() > 0 ? true : false;                    // returns true if the delete operation was successful
        
        }

        #endregion


        #region Manual Mapping
        //public IEnumerable<DepartmentDto> GetAllDepartments()
        //{
        //    var departments = _departmentRepository.GetAll();
        //    return departments.Select(d => new DepartmentDto
        //    {
        //        Id = d.Id,
        //        Name = d.Name,
        //        Code = d.Code,
        //        Description = d.Description,
        //        DateOfCreation = DateOnly.FromDateTime(d.CreatedOn)
        //    });
        //}


        //public DepartmentDetailsDto? GetDepartmentById(int id)
        //{
        //    var department = _departmentRepository.GetById(id);

        //    return department is null ? null : new DepartmentDetailsDto          // ternary operator for null check
        //    {
        //        Id = department.Id,
        //        Name = department.Name,
        //        Code = department.Code,
        //        Description = department.Description,
        //        CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
        //        UpdatedOn = DateOnly.FromDateTime(department.LastModifiedOn),
        //        CreatedBy = department.CreatedBy,
        //        UpdatedBy = department.LastModifiedBy,
        //        IsDeleted = department.IsDeleted
        //    };
        //}

        #endregion



    }
}
