namespace WebApplication2.Services.Implement;

using AutoMapper;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
using WebApplication2.UnitOfWork;
using WebApplication2.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BookingService : IBookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookingDto> CreateBookingAsync(BookingDto dto)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(dto.RoomId);
        if (room == null || !room.IsAvailable)
            throw new InvalidOperationException("Room not available");

        var booking = _mapper.Map<Booking>(dto);
        booking.Id = Guid.NewGuid().ToString();
        booking.IsCancelled = false;

        await _unitOfWork.Bookings.AddAsync(booking);
        room.IsAvailable = false;
        await _unitOfWork.SaveAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(string userId)
    {
        var bookings = await _unitOfWork.Bookings.GetBookingsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<BookingDto>>(bookings);
    }

    public async Task<BookingDto> GetBookingByIdAsync(string id)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
        return booking == null ? null : _mapper.Map<BookingDto>(booking);
    }

    public async Task CancelBookingAsync(string bookingId)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null || booking.IsCancelled==true) return;

        booking.IsCancelled = true;

        var room = await _unitOfWork.Rooms.GetByIdAsync(booking.RoomId);
        if (room != null) room.IsAvailable = true;

        await _unitOfWork.SaveAsync();
    }
}