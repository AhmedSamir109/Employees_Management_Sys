using EmpsManagement.BLL.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.Services.Employee
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDto? GetbyId(int id);
        int createEmployee(CreateEmployeeDto departmentDto);
        int UpdateEmployee(UpdateEmployeeDto departmentDto);
        bool DeleteEmployee(int id);
    }
}
