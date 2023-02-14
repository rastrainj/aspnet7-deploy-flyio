using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DataDbContext(DbContextOptions<DataDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDatabase"));
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public DbSet<User> Users { get; set; }
}