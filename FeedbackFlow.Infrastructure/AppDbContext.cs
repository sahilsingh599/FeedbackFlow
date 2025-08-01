using FeedbackFlow.Core;
using Microsoft.EntityFrameworkCore;

namespace FeedbackFlow.Infrastructure;

/// <summary>
/// The Entity Framework database context for the application.
/// It defines the tables (DbSets) that EF Core will manage.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // This DbSet will be translated into a "FeedbackItems" table in the database.
    public DbSet<FeedbackItem> FeedbackItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the FeedbackItem entity
        modelBuilder.Entity<FeedbackItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Source).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Author).HasMaxLength(100);

            // This will create an index on the ReceivedAt column for faster sorting.
            entity.HasIndex(e => e.ReceivedAt);

            // Configure the Topics property for JSON serialization
            entity.Property(e => e.Topics)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        });
    }
}