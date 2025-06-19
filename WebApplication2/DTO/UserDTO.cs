namespace WebApplication2.DTO;

public class UserDto
{
    public string Id { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } = "User"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<BookingDto>? Bookings { get; set; } 
}
public class UserRegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User"; 
}

public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}