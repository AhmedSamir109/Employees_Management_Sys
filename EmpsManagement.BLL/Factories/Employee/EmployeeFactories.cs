using EmpsManagement.BLL.DTOs.Employee;
using EmpsManagement.DAL.Models.Departments;
using EmpsManagement.DAL.Models.Employees;

namespace EmpsManagement.BLL.Factories
{
    public static class EmployeeFactories
    {

        #region DTOs for (Reading) from DAL To PL across BLL

        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Email = employee.Email,
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType.ToString(),
                Gender = employee.Gender.ToString(),
                DepartmentName = employee.Department?.Name 


            };
        }


        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType.ToString(),
                Gender = employee.Gender.ToString(),
                CreatedOn = employee.CreatedOn,
                CreatedBy = employee.CreatedBy,
                UpdatedOn = employee.LastModifiedOn,
                UpdatedBy = employee.LastModifiedBy,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name,
                ImageName = employee.ImageName // Assuming you want to return the image name


            };
        }

        #endregion


        #region DTOs for (Writing) From PL To DAL across BLL

        public static Employee ToEntity(this CreateEmployeeDto employee , string ImageName)
        {
            return new Employee()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate.ToDateTime(new TimeOnly()),
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                DepartmentId = employee.DepartmentId,
                ImageName = ImageName // Assuming you want to store the image name

            };
        }
        public static Employee ToEntity(this UpdateEmployeeDto employee)
        {
            return new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate.ToDateTime(new TimeOnly()),
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                DepartmentId = employee.DepartmentId,
                ImageName = employee.Image?.FileName // Assuming you want to store the image name
            };
        }
        #endregion
    }
}
