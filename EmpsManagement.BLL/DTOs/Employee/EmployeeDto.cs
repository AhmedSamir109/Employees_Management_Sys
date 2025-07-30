using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;
        public int Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Employee Type")]
        public  string EmployeeType { get; set; }

        [Display (Name = "Is Active")]
        public bool IsActive { get; set; }

        public string? DepartmentName { get; set; }
    }
}
