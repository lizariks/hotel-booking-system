namespace WebApplication2.Repository;

using Microsoft.EntityFrameworkCore;
using WebApplication2.Enteties;


    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly HotelDbContext _context;

        public ReviewRepository(HotelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
