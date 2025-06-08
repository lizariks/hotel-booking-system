namespace WebApplication2.Enteties;

public class Review
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}