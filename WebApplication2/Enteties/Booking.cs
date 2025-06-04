namespace WebApplication2.Enteties;

public class Booking
{
    public int Id { get; set; }
    
    public string UserId { get; set; } = null!;
    public User User { get; set; }
    
    public string RoomId { get; set; } = null!;
    public Room Room { get; set; }
    
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public bool? IsCancelled { get; set; }
}