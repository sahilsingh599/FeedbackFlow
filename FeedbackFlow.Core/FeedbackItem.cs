namespace FeedbackFlow.Core;

/// <summary>
/// Represents a single piece of customer feedback, normalized from any source.
/// This is the central entity of our application.
/// </summary>
public class FeedbackItem
{
    public Guid Id { get; set; }
    public string Source { get; set; } = string.Empty; // e.g., "Twitter", "AppStore"
    public string Content { get; set; } = string.Empty; // The actual feedback text
    public string Author { get; set; } = string.Empty; // User who gave the feedback
    public DateTimeOffset ReceivedAt { get; set; }

    // --- Properties to be populated by the Analysis Service ---
    public string? Sentiment { get; set; } // e.g., "Positive", "Negative", "Neutral"
    public List<string> Topics { get; set; } = new(); // e.g., ["UI/UX", "Bug", "Pricing"]
    public bool IsAnalyzed { get; set; } = false;

    public FeedbackItem()
    {
        Id = Guid.NewGuid();
        ReceivedAt = DateTimeOffset.UtcNow;
    }
}