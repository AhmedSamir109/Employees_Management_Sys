using EmpsManagement.DAL.Models.Employees;
using EmpsManagement.DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.BLL.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(3, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = string.Empty;

        [Range(22, 45)]
        public int Age { get; set; }

        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}$",
         ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [EmailAddress]   // name@Domain.com
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }
    }
}
