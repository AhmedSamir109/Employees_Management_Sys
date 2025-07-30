using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.BLL.DTOs.Department
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly CreatedOn { get; set; }
        public DateOnly? UpdatedOn { get; set; }

        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
