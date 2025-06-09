using System.Net;

namespace WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync();
    Task<RoomDto> GetRoomByIdAsync(string id);
    Task AddRoomAsync(RoomDto roomDto);
    Task UpdateRoomAsync(RoomDto roomDto);
    Task DeleteRoomAsync(string Id);
    
}
