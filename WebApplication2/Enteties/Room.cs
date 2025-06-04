namespace WebApplication2.Enteties;

public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public string? Description { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }
}