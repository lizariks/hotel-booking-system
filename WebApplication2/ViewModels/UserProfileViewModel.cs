namespace WebApplication2.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
public class UserProfileViewModel
{
    public string Id { get; set; }
        
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
        
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
        
    [Display(Name = "Email")]
    public string Email { get; set; }
        
    [Display(Name = "Role")]
    public string Role { get; set; }
        
    [Display(Name = "Member Since")]
    public DateTime CreatedAt { get; set; }
        
    [Display(Name = "Total Bookings")]
    public int BookingCount { get; set; }
}