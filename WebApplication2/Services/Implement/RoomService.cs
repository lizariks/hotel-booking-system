using AutoMapper; 
using WebApplication2.DTO;
using WebApplication2.Enteties;
using WebApplication2.Services.Interfaces;
using WebApplication2.UnitOfWork;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync()
    {
        var rooms = await _unitOfWork.Rooms.GetAllAsync();
        var availableRooms = rooms.Where(r => r.IsAvailable);
        return _mapper.Map<IEnumerable<RoomDto>>(availableRooms);
    }

    public async Task<RoomDto> GetRoomByIdAsync(string id)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(id);
        return room == null ? null : _mapper.Map<RoomDto>(room);
    }

    public async Task AddRoomAsync(RoomDto roomDto)
    {
        var room = _mapper.Map<Room>(roomDto);
        room.Id = Guid.NewGuid().ToString(); 
        await _unitOfWork.Rooms.AddAsync(room);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateRoomAsync(RoomDto roomDto)
    {
        var room = await _unitOfWork.Rooms.GetByIdAsync(roomDto.Id);
        if (room == null) return;

        _mapper.Map(roomDto, room); 
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

        return _mapper.Map<IEnumerable<RoomDto>>(query.ToList());
    }
}
