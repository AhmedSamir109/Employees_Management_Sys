using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.BLL.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required (ErrorMessage = "Name is required !!!")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(100 , int.MaxValue)]
        public String Code { get; set; } = string.Empty ;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
