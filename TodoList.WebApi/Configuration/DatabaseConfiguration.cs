using Microsoft.EntityFrameworkCore;
using TodoList.DatabaseManager.Managers.Postgresql;

namespace TodoList.WebApi.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            options.UseNpgsql(configuration.GetConnectionString("TodoListDatabase"))
                .UseSnakeCaseNamingConvention());
    }
}