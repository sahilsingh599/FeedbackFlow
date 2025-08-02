using FeedbackFlow.Api.Services;
using FeedbackFlow.Api.Workers;
using FeedbackFlow.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Add services to the dependency injection container. ---

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- NEW: Register our AI services ---
// Add the AnalysisService as a Singleton because loading the ML model is expensive.
builder.Services.AddSingleton<AnalysisService>();
// Add the background worker that will use the AnalysisService.
builder.Services.AddHostedService<AnalysisWorker>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// --- 2. Configure the HTTP request pipeline. ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowWebApp");
app.UseAuthorization();
app.MapControllers();

// Seeding logic remains the same...
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.FeedbackItems.Any())
    {
        context.FeedbackItems.AddRange(
            new FeedbackFlow.Core.FeedbackItem { Source = "Twitter", Author = "@jane_doe", Content = "Just tried the new feature, it's amazing! So much faster now." },
            new FeedbackFlow.Core.FeedbackItem { Source = "AppStore", Author = "User123", Content = "The latest update keeps crashing on my iPhone. Please fix this!" },
            new FeedbackFlow.Core.FeedbackItem { Source = "Zendesk", Author = "john.doe@email.com", Content = "I'm having trouble understanding the pricing structure. Can someone explain the Enterprise tier?" }
        );
        context.SaveChanges();
    }
}

app.Run();