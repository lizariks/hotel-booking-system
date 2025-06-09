namespace WebApplication2.DTO;

public class BookingDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public bool? IsCancelled { get; set; }
}

