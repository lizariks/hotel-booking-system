namespace WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
public interface IReviewService
{
    Task AddReviewAsync(ReviewDto reviewDto);
    Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
    Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(string userId);
    
}
