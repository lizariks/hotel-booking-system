namespace WebApplication2.Repository;
using WebApplication2.Enteties;
public class RoomRepository:GenericRepository<Room>, IRoomRepository
{
    public RoomRepository(HotelDbContext context) : base(context) { }
}