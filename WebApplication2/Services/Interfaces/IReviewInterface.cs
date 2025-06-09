namespace WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
public interface IReviewService
{
    Task AddReviewAsync(ReviewDto reviewDto);
}
