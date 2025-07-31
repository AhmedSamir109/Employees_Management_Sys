using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.PL.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "first name is required.")]
        [MaxLength(50 , ErrorMessage = "max number of char are 50")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required.")]
        [MaxLength(50, ErrorMessage = "max number of char are 50")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "user name is required.")]
        //[MaxLength(50, ErrorMessage = "max number of char are 50")]
        //public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
