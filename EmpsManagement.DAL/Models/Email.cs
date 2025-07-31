using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpsManagement.DAL.Models
{
    [NotMapped]
    public class Email
    {
        public int Id { get; set; }
        public required string ToWhom { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}
