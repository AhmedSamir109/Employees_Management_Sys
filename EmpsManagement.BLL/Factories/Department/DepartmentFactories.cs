using EmpsManagement.BLL.DTOs.Department;
using EmpsManagement.DAL.Models.Departments;

namespace EmpsManagement.BLL.Factories
{
    public static class DepartmentFactories
    {
        #region DTOs for (Reading) from DAL To PL across BLL
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn)
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                CreatedBy = department.CreatedBy,
                UpdatedOn = DateOnly.FromDateTime(department.LastModifiedOn),
                UpdatedBy = department.LastModifiedBy,
                IsDeleted = department.IsDeleted,
            };
        }

        #endregion

        #region DTOs for (Writing) From PL To DAL across BLL
        public static Department ToEntity(this CreateDepartmentDto department)
        {
            return new Department()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = department.DateOfCreation.ToDateTime(new TimeOnly()),
            };
        }

        public static Department ToEntity(this UpdateDepartmentDto department)
        {
            return new Department()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = department.DateOfCreation.ToDateTime(new TimeOnly()),
            };
        } 

        #endregion

    }
}
