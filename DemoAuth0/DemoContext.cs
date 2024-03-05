using Microsoft.EntityFrameworkCore;

namespace DemoAuth0;

public class DemoContext : DbContext
{
    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
    }

    public DbSet<User> NewUsers { get; set; }
}
