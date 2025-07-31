using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.PL.ViewModels.User
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "First Name")]
        public string FName { get; set; }  
        
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public IEnumerable<string>? Roles { get; set; }


    }                                                                                         
}
