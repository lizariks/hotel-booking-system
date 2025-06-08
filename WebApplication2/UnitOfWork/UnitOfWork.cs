namespace WebApplication2.UnitOfWork;
using WebApplication2.Enteties;
using WebApplication2.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly HotelDbContext _context;

    public IBookingRepository Bookings { get; private set; }
    public IGenericRepository<User> Users { get; private set; }
    public IGenericRepository<Room> Rooms { get; private set; }
    public IReviewRepository Reviews { get; private set; }

    public UnitOfWork(HotelDbContext context)
    {
        _context = context;
        Bookings = new BookingRepository(_context);
        Users = new GenericRepository<User>(_context);
        Rooms = new GenericRepository<Room>(_context);
        Reviews = new ReviewRepository(_context);
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
