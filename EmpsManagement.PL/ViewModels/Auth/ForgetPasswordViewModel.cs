using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
    }
}
