using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.DTO;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers;

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
    public async Task<IActionResult> GetBookingById(string id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null)
            return NotFound(new { message = "Booking not found" });

        return Ok(booking);
    }
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bookings = await _bookingService.GetBookingsByUserAsync(userId);
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bookingDto.UserId = userId;

        try
        {
            var result = await _bookingService.CreateBookingAsync(bookingDto);
            return CreatedAtAction(nameof(GetBookingById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelBooking(string id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null)
            return NotFound(new { message = "Booking not found" });

        await _bookingService.CancelBookingAsync(id);
        return Ok(new { message = "Booking cancelled successfully" });
    }
}
