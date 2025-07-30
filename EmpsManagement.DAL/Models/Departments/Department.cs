using EmpsManagement.DAL.Models.Employees;

namespace EmpsManagement.DAL.Models.Departments
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }


        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
