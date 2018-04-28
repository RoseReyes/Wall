using System;
using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class User
    {
            [Required(ErrorMessage="First name is required")]
            [Display(Name = "First Name")]  
            [MinLength(2)]
            public string FirstName { get; set; }

            [Required(ErrorMessage="Last name is required")]
            [Display(Name = "Last Name")] 
            [MinLength(2)] 
            public string LastName { get; set; }
            
            [Required(ErrorMessage="Email is required")]
            [Display(Name = "Email")]   
            [EmailAddress]
            [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")] 
            public string Email { get; set; }

            [Required(ErrorMessage="Password is required")] 
            [Display(Name = "Password")] 
            [MinLength(8)]
            [DataType(DataType.Password)] public string Password { get; set; }

            [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
            [Display(Name = "Confirm Password")]
            [DataType(DataType.Password)] public string ConfirmPassword { get; set; }
    }
}