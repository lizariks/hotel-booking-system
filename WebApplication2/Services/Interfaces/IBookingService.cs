namespace WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
public interface IBookingService
{
    Task<BookingDto> CreateBookingAsync(BookingDto bookingDto);
    Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(string userId);
    Task CancelBookingAsync(string bookingId);
    Task<BookingDto> GetBookingByIdAsync(string id);

}