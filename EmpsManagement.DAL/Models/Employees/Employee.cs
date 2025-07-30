using EmpsManagement.DAL.Models.Departments;
using EmpsManagement.DAL.Models.Shared.Enums;

namespace EmpsManagement.DAL.Models.Employees
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public string? ImageName { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
