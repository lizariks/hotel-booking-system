namespace WebApplication2.Services.Implement;

using AutoMapper;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
using WebApplication2.UnitOfWork;
using WebApplication2.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddReviewAsync(ReviewDto reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        review.Id = Guid.NewGuid().ToString();
        review.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.Reviews.AddAsync(review);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
    {
        var reviews = await _unitOfWork.Reviews.GetAllAsync();
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(string userId)
    {
        var reviews = await _unitOfWork.Reviews.GetReviewsByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }
}