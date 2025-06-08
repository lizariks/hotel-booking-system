namespace WebApplication2.Enteties;

public class Booking
{
    public string Id { get; set; }
    
    public string UserId { get; set; } 
    public User User { get; set; }
    
    public string RoomId { get; set; } 
    public Room Room { get; set; }
    
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public bool? IsCancelled { get; set; }
}