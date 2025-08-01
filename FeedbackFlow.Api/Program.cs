using FeedbackFlow.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Add services to the dependency injection container. ---

// Add User Secrets configuration for development
// This allows us to store secrets like connection strings outside of the project tree.
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add a PostgreSQL database context.
// It reads the connection string from configuration (appsettings.json, then user secrets, then environment variables).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add API controllers.
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API documentation and testing.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy to allow our Next.js frontend to call the API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // The default Next.js port
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// --- 2. Configure the HTTP request pipeline. ---

// Use Swagger in development for easy API testing.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy.
app.UseCors("AllowWebApp");

app.UseAuthorization();

// Map controller routes.
app.MapControllers();

// --- Seed the database for demonstration purposes ---
// This is a simple way to add some data on startup for testing.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    // Ensure the database is created.
    context.Database.EnsureCreated();

    // Check if there is already data.
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