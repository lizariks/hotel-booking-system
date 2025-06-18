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
                Description = r.Description,
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
            Description = room.Description,
            PricePerNight = room.PricePerNight,
            IsAvailable = room.IsAvailable
        };
    }
    public async Task AddRoomAsync(RoomDto roomDto)
    {
        var room = new Room
        {
            Id = Guid.NewGuid().ToString(),
            RoomNumber = roomDto.RoomNumber,
            RoomType = roomDto.RoomType,
            Description = roomDto.Description,
            PricePerNight = roomDto.PricePerNight,
            IsAvailable = roomDto.IsAvailable
        };

        await _unitOfWork.Rooms.AddAsync(room);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateRoomAsync(RoomDto roomDto)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(roomDto.Id);
        if (room == null) return;

        room.RoomNumber = roomDto.RoomNumber;
        room.RoomType = roomDto.RoomType;
        room.Description = roomDto.Description;
        room.PricePerNight = roomDto.PricePerNight;
        room.IsAvailable = roomDto.IsAvailable;

        _unitOfWork.Rooms.Update(room);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteRoomAsync(string id)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(id);
        if (room == null) return;

        _unitOfWork.Rooms.Delete(room);
        await _unitOfWork.SaveAsync();
    }
    
    public async Task<IEnumerable<RoomDto>> GetFilteredRoomsAsync(RoomFilterDto filter)
    {
        var rooms = await _unitOfWork.Rooms.GetAllAsync();
        var query = rooms.AsQueryable();

        if (filter.RoomType.HasValue)
            query = query.Where(r => r.RoomType == filter.RoomType.Value);

        if (filter.MinPrice.HasValue)
            query = query.Where(r => r.PricePerNight >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(r => r.PricePerNight <= filter.MaxPrice.Value);

        if (filter.IsAvailable.HasValue)
            query = query.Where(r => r.IsAvailable == filter.IsAvailable.Value);

        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query = filter.SortBy.ToLower() switch
            {
                "price" => filter.SortDirection == "desc"
                    ? query.OrderByDescending(r => r.PricePerNight)
                    : query.OrderBy(r => r.PricePerNight),
                "roomnumber" => filter.SortDirection == "desc"
                    ? query.OrderByDescending(r => r.RoomNumber)
                    : query.OrderBy(r => r.RoomNumber),
                _ => query
            };
        }

        query = query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        return query.Select(r => new RoomDto
        {
            Id = r.Id,
            RoomNumber = r.RoomNumber,
            RoomType = r.RoomType,
            PricePerNight = r.PricePerNight,
            IsAvailable = r.IsAvailable
        }).ToList();
    }

}
