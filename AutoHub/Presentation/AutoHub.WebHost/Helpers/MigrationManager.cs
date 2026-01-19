using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoHub.WebHost.Helpers;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase<T>(this WebApplication app) where T : DbContext
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<MigrationManager>>();
        var db = services.GetRequiredService<T>();

        logger.LogInformation("Applying migrations for {DbContext}", typeof(T).FullName);
        db.Database.Migrate();
        logger.LogInformation("Migrations applied successfully for {DbContext}", typeof(T).FullName);

        return app;
    }
}