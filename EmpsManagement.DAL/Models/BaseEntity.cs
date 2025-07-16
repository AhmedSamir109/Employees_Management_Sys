using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }   

        public int CreatedBy { get; set; }   // UserID
        public DateTime? CreatedOn { get; set; } 

        public int? LastModifiedBy { get; set; }  // UserID
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }        // Soft Delete


    }
}
