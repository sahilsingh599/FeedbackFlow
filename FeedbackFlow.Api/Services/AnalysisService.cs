using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace FeedbackFlow.Api.Services;

/// <summary>
/// A service to perform machine learning analysis on feedback text.
/// </summary>
public class AnalysisService
{
    private readonly PredictionEngine<ModelInput, ModelOutput> _predictionEngine;

    public AnalysisService()
    {
        var mlContext = new MLContext();

        // ML.NET comes with a pre-trained sentiment model. We just need to load it.
        // This is a simplified approach. In a real app, you'd train your own model.
        // For this project, we'll simulate the prediction.
    }

    // Defines the input schema for our model.
    public class ModelInput
    {
        public string Text { get; set; } = "";
    }

    // Defines the output schema of our model.
    public class ModelOutput
    {
        // A value of 0 is negative, 1 is positive.
        public uint PredictedLabel { get; set; }
    }

    /// <summary>
    /// Predicts the sentiment of a given text.
    /// </summary>
    /// <param name="text">The feedback text to analyze.</param>
    /// <returns>"Positive" or "Negative"</returns>
    public string PredictSentiment(string text)
    {
        // --- SIMULATION LOGIC ---
        // Since loading a pre-built model can be complex for a portfolio piece setup,
        // we'll simulate the prediction with simple keyword matching.
        // This demonstrates the architecture without the ML ops overhead.
        // You can explain this simplification in your project's README.
        var positiveKeywords = new[] { "amazing", "love", "great", "fast", "easy", "excellent" };
        var negativeKeywords = new[] { "crashing", "fix", "slow", "hard", "bad", "error", "trouble" };

        var lowerText = text.ToLowerInvariant();

        if (negativeKeywords.Any(keyword => lowerText.Contains(keyword)))
        {
            return "Negative";
        }
        if (positiveKeywords.Any(keyword => lowerText.Contains(keyword)))
        {
            return "Positive";
        }

        return "Neutral";
    }
}