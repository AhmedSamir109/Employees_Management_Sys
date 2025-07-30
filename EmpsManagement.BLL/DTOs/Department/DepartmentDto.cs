
namespace EmpsManagement.BLL.DTOs.Department
{
    public class DepartmentDto
    {
        // CTOR Mapping

        //public DepartmentDto(Department department)
        //{
        //    Id = department.Id;
        //    Name = department.Name;
        //    Code = department.Code;
        //    Description = department.Description;
        //    DateOfCreation = DateOnly.FromDateTime(department.CreatedOn);
        //}

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
