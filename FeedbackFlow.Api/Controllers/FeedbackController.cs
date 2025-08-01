using FeedbackFlow.Core;
using FeedbackFlow.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbackFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<FeedbackController> _logger;

    public FeedbackController(AppDbContext context, ILogger<FeedbackController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Gets a list of all feedback items.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackItem>>> GetFeedbackItems()
    {
        try
        {
            var feedback = await _context.FeedbackItems
                                        .OrderByDescending(f => f.ReceivedAt)
                                        .ToListAsync();
            return Ok(feedback);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching feedback items.");
            return StatusCode(500, "Internal server error");
        }
    }
}