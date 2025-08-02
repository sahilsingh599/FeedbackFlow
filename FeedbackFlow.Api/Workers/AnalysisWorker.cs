using FeedbackFlow.Api.Services;
using FeedbackFlow.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FeedbackFlow.Api.Workers;

/// <summary>
/// A background service that periodically analyzes new feedback items.
/// </summary>
public class AnalysisWorker : IHostedService, IDisposable
{
    private readonly ILogger<AnalysisWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer;

    public AnalysisWorker(ILogger<AnalysisWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Analysis Worker is starting.");
        // Run the task every 10 seconds.
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        _logger.LogInformation("Analysis Worker is running.");

        // We need to create a new scope to resolve scoped services like AppDbContext.
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var analysisService = scope.ServiceProvider.GetRequiredService<AnalysisService>();

            // Find feedback that hasn't been analyzed yet.
            var itemsToAnalyze = await dbContext.FeedbackItems
                .Where(f => !f.IsAnalyzed)
                .ToListAsync();

            if (!itemsToAnalyze.Any())
            {
                _logger.LogInformation("No new feedback to analyze.");
                return;
            }

            _logger.LogInformation($"Found {itemsToAnalyze.Count} items to analyze.");
            foreach (var item in itemsToAnalyze)
            {
                item.Sentiment = analysisService.PredictSentiment(item.Content);
                item.IsAnalyzed = true;
            }

            await dbContext.SaveChangesAsync();
            _logger.LogInformation("Analysis complete. Updated items in the database.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Analysis Worker is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}