using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.DTOs.Employee
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public bool IsActive { get; set; }
        public string EmployeeType { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public int? DepartmentId { get; set; }

        [Display (Name = "Department Name")]
        public string? DepartmentName { get; set; }
        public string? ImageName { get; set; }
    }
}
