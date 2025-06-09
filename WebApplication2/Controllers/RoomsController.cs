namespace WebApplication2.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;



[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableRooms()
    {
        var rooms = await _roomService.GetAvailableRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(string id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RoomDto dto)
    {
        await _roomService.AddRoomAsync(dto);
        return StatusCode(201); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(string id, [FromBody] RoomDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch");
        await _roomService.UpdateRoomAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(string id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }
}
