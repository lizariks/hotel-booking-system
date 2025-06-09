using WebApplication2.Services.Interfaces;

namespace WebApplication2.Services.Implement;
using WebApplication2.DTO;
using WebApplication2.UnitOfWork;
using WebApplication2.Enteties;
using WebApplication2.Services.Interfaces;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BookingDto> CreateBookingAsync(BookingDto dto)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(dto.RoomId);
        if (room == null || !room.IsAvailable)
            throw new InvalidOperationException("Room not available");

        var booking = new Booking
        {
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            CheckInDate = dto.CheckInDate,
            CheckOutDate = dto.CheckOutDate,
            TotalPrice = dto.TotalPrice,
            IsCancelled = false
        };

        await _unitOfWork.Bookings.AddAsync(booking);
        room.IsAvailable = false;
        await _unitOfWork.SaveAsync();

        dto.Id = booking.Id;
        return dto;
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(string userId)
    {
        var bookings = await _unitOfWork.Bookings.GetBookingsByUserIdAsync(userId);

        return bookings.Select(b => new BookingDto
        {
            Id = b.Id,
            UserId = b.UserId,
            RoomId = b.RoomId,
            CheckInDate = b.CheckInDate,
            CheckOutDate = b.CheckOutDate,
            TotalPrice = b.TotalPrice,
            IsCancelled = b.IsCancelled
        });
    }

    public async Task CancelBookingAsync(string bookingId)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null || booking.IsCancelled == true) return;

        booking.IsCancelled = true;

        var room = await _unitOfWork.Rooms.GetByIdAsync(booking.RoomId);
        if (room != null) room.IsAvailable = true;

        await _unitOfWork.SaveAsync();
    }
}

