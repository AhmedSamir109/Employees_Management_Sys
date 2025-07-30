﻿using System.ComponentModel.DataAnnotations;

namespace EmpsManagement.PL.ViewModels.Auth
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
