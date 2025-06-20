namespace WebApplication2.DTO;

using System.ComponentModel.DataAnnotations;

public class BookingDto
{
    public string Id { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public string RoomId { get; set; }
    [Required]
    public DateTime CheckInDate { get; set; }
    [Required]
    public DateTime CheckOutDate { get; set; }
    [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
    public decimal TotalPrice { get; set; }
    public bool? IsCancelled { get; set; }
}
public class CreateBookingDto
{
    [Required]
    public string RoomId { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
    public decimal TotalPrice { get; set; }
}


