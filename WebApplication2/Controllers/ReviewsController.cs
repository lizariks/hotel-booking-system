namespace WebApplication2.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;
using WebApplication2.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService; // Fixed: removed asterisk

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

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyReviews()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reviews = await _reviewService.GetReviewsByUserIdAsync(userId); 
        return Ok(reviews);
    }
}