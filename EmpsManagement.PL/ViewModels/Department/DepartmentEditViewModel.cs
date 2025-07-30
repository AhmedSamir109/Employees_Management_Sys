using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.PL.ViewModels.Department
{
    public class DepartmentEditViewModel
    {
        [Required (ErrorMessage = "Name is required !")]
        public string Name { get; set; } = string.Empty;
        [Required (ErrorMessage = "Code is required !")]
        [Range( 100 , int.MaxValue) ]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
