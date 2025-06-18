namespace WebApplication2.UnitOfWork;
using WebApplication2.Enteties;
using WebApplication2.Repository;
public interface IUnitOfWork : IDisposable
{
    IBookingRepository Bookings { get; }
    IGenericRepository<User> Users { get; }
    Task<int> SaveAsync();
    IReviewRepository Reviews { get; }
    IRoomRepository Rooms { get; }

}
