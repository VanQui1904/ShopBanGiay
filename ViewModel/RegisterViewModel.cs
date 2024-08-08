using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeStoreProject.ViewModel
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }
    }
}