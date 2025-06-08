namespace WebApplication2.Repository;
using WebApplication2.Enteties;
using Microsoft.EntityFrameworkCore;
public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(HotelDbContext context) : base(context) { }

    public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
    {
        return await _dbSet
            .Where(b => b.UserId == userId)
            .Include(b => b.Room)
            .ToListAsync();
    }
}
