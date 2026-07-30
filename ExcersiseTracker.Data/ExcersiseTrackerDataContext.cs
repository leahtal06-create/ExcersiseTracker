using Microsoft.EntityFrameworkCore;

namespace ExcersiseTracker.Data;

public class ExcersiseTrackerDataContext : DbContext
{
    private readonly string _connectionString;

    public ExcersiseTrackerDataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    public DbSet<User> User { get; set; }
}