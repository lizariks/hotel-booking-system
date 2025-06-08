using Microsoft.AspNetCore.Mvc;
using WebApplication2.Enteties;
using WebApplication2.UnitOfWork;

namespace WebApplication2.Controllers;


[Route("api/[controller]")]
[ApiController]
public class HotelBookingController:ControllerBase
{
    private readonly HotelDbContext _context;

    public HotelBookingController(HotelDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public JsonResult CreateEdit(Booking booking)
    {
        Room room = new Room();
        var days = (booking.CheckOutDate - booking.CheckInDate).Days;
        booking.TotalPrice = room.PricePerNight * days;

        if (booking.Id == null)
        {
            _context.Bookings.Add(booking);
        }
        else
        {
            var bookingInDb=_context.Bookings.Find(booking.Id);
            if (bookingInDb == null)
                return new JsonResult(NotFound());
            bookingInDb = booking;
        }
        _context.SaveChanges();
        return new JsonResult(Ok(booking));
    }
}
public class BookingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetBookingsByUser(string userId)
    {
        var bookings = await _unitOfWork.Bookings.GetBookingsByUserIdAsync(userId);
        return Ok(bookings);
    }
}
