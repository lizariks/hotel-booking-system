namespace WebApplication2.Enteties;
using System.Collections.Generic;
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public string Role { get; set; } = "Customer";
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}