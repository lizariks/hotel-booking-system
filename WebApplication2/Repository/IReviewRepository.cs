namespace WebApplication2.Repository;

using WebApplication2.Enteties;

    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(string userId);
    }

