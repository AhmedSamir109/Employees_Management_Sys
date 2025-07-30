namespace EmpsManagement.DAL.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; }   

        public int CreatedBy { get; set; }   // UserID
        public DateTime CreatedOn { get; set; } 

        public int? LastModifiedBy { get; set; }  // UserID
        public DateTime LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }        // Soft Delete


    }
}
