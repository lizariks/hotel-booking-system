namespace WebApplication2.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
using WebApplication2.Enteties;



[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<BookingDto>> GetBookingById(string id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null)
            return NotFound();

        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
    {
        var result = await _bookingService.CreateBookingAsync(bookingDto);
        if (result==null)
            return BadRequest();
        return CreatedAtAction(nameof(GetBookingById), new { id = result.Id }, result);
    }
    
}
