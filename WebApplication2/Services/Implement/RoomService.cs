namespace WebApplication2.Services.Implement;
using WebApplication2.Enteties;
using WebApplication2.Services.Interfaces;
using WebApplication2.UnitOfWork;
using WebApplication2.Enteties;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DTO;
public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync()
    {
        var rooms = await _unitOfWork.Rooms.GetAllAsync();
        return rooms
            .Where(r => r.IsAvailable)
            .Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
                IsAvailable = r.IsAvailable
            });
    }

    public async Task<RoomDto> GetRoomByIdAsync(string id)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(id);
        if (room == null) return null;

        return new RoomDto
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType,
            PricePerNight = room.PricePerNight,
            IsAvailable = room.IsAvailable
        };
    }
}
