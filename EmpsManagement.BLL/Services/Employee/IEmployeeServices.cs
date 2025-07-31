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
        int createEmployee(CreateEmployeeDto employeeDto);
        int UpdateEmployee(UpdateEmployeeDto employeeDto , string? oldImageName);
        bool DeleteEmployee(int id);
    }
}
