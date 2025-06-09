namespace WebApplication2.DTO;
using WebApplication2.Enteties;
public class RoomDto
{
    public string Id { get; set; }
    public string RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }
}
