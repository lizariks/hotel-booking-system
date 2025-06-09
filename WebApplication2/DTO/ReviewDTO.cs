namespace WebApplication2.DTO;

public class ReviewDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string RoomId { get; set; }
    public decimal Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}

