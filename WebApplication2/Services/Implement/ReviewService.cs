namespace WebApplication2.Services.Implement;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
using WebApplication2.UnitOfWork;
using WebApplication2.Repository;
using WebApplication2.Enteties;
public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
    public async Task AddReviewAsync(ReviewDto reviewDto)
    {
        var review = new Review
        {
            Id = Guid.NewGuid().ToString(),
            UserId = reviewDto.UserId,
            RoomId = reviewDto.RoomId,
            Rating = reviewDto.Rating,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Reviews.AddAsync(review);
        await _unitOfWork.SaveAsync();
    }
    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
    {
        var reviews = await _unitOfWork.Reviews.GetAllAsync();
        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            UserId = r.UserId,
            RoomId = r.RoomId,
            Rating = r.Rating,
            CreatedAt = r.CreatedAt
        });
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(string userId)
    {
        var reviews = await _unitOfWork.Reviews.GetReviewsByUserIdAsync(userId);
        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            UserId = r.UserId,
            RoomId = r.RoomId,
            Rating = r.Rating,
            CreatedAt = r.CreatedAt
        });
    }

}
