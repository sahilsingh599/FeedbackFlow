using Microsoft.AspNetCore.Mvc;

namespace FeedbackFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public SettingsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Demonstrates how to read a configured value.
    /// NOTE: NEVER expose an API key in a real-world API endpoint like this.
    /// This is for demonstration purposes only.
    /// </summary>
    [HttpGet("twitter-api-key")]
    public IActionResult GetTwitterApiKey()
    {
        // This reads a nested configuration value using ":" as a separator.
        // The configuration provider will look in appsettings.json, then user secrets, etc.
        var key = _configuration["ApiKeys:Twitter"];

        if (string.IsNullOrEmpty(key) || key.Contains("YOUR_KEY_HERE"))
        {
            return Ok(new { status = "Not Set", message = "Twitter API Key is not set in your configuration (user secrets)." });
        }

        // We only show a portion of the key to prove it's loaded.
        return Ok(new { status = "Loaded", message = $"The configured key starts with: {key.Substring(0, 4)}..." });
    }
}