namespace WebApplication2.Repository;
using WebApplication2.Enteties;
public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId);
}
