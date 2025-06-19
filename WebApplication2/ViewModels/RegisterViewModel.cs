namespace WebApplication2.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
public class RegisterViewModel
{
    
            [Required(ErrorMessage = "First name is required.")]
            [Display(Name = "First Name")]
            [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
            public string FirstName { get; set; }
    
            [Required(ErrorMessage = "Last name is required.")]
            [Display(Name = "Last Name")]
            [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
            public string LastName { get; set; }
    
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            [Display(Name = "Email")]
            public string Email { get; set; }
    
            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
            public string Password { get; set; }
    
            [Required(ErrorMessage = "Please confirm your password.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
    
            [Required(ErrorMessage = "Please select a role.")]
            [Display(Name = "Role")]
            public string SelectedRole { get; set; } = "Customer";
    
            public List<SelectListItem> Roles { get; set; } = new()
            {
                new SelectListItem { Text = "Customer", Value = "Customer" },
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "Manager", Value = "Manager" }
            };
        }
        