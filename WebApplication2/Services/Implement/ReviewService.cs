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
            Rating = reviewDto.Rating,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Reviews.AddAsync(review);
        await _unitOfWork.SaveAsync();
    }
}
