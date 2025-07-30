using EmpsManagement.BLL.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.Services
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetbyId(int id);
        int createDepartment(CreateDepartmentDto departmentDto);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
    }
}
