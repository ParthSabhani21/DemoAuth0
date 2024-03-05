using Microsoft.EntityFrameworkCore;

namespace DemoAuth0.Config;
public static class SqlServer
{
    public static void AddSqlServerConfig(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration["ConnectionStrings:DefaultConnection"];

        services.AddDbContext<DemoContext>(options => options.UseSqlServer(connectionString));

    }
}

