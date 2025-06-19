namespace WebApplication2.DTO;


public class UserWithRolesDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<string> Roles { get; set; } = new();
    public string RolesString => Roles.Any() ? string.Join(", ", Roles) : "No roles";
}
