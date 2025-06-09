namespace WebApplication2.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;


[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
    {
        await _reviewService.AddReviewAsync(dto);
        return StatusCode(201); 
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await _reviewService.GetAllReviewsAsync();
        return Ok(reviews);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReviewsByUser(string userId)
    {
        var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
        return Ok(reviews);
    }
}

