using FeedbackFlow.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbackFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(AppDbContext context, ILogger<AnalyticsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// A DTO (Data Transfer Object) to hold the result of our analytics query.
    /// </summary>
    public class SentimentDistributionResult
    {
        public string? Sentiment { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Gets the count of feedback items for each sentiment type.
    /// </summary>
    [HttpGet("sentiment-distribution")]
    public async Task<ActionResult<IEnumerable<SentimentDistributionResult>>> GetSentimentDistribution()
    {
        try
        {
            var sentimentDistribution = await _context.FeedbackItems
                .Where(f => f.IsAnalyzed && f.Sentiment != null) // Only include analyzed items
                .GroupBy(f => f.Sentiment)
                .Select(g => new SentimentDistributionResult
                {
                    Sentiment = g.Key,
                    Count = g.Count()
                })
                .OrderBy(r => r.Sentiment)
                .ToListAsync();

            return Ok(sentimentDistribution);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching sentiment distribution.");
            return StatusCode(500, "Internal server error");
        }
    }
}