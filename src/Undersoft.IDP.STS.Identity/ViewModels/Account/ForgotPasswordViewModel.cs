﻿using System.ComponentModel.DataAnnotations;
using Undersoft.IDP.Shared.Configuration.Configuration.Identity;

namespace Undersoft.IDP.STS.Identity.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public LoginResolutionPolicy? Policy { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }
    }
}
