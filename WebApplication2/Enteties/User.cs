namespace WebApplication2.Enteties;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
public class User : IdentityUser
{
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public string Role { get; set; } = "Customer";
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}