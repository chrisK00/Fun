using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemorySignal.Core.Extensions;
public static class ServicesExtensions
{
    public static void AddCoreServices(this IServiceCollection services, IConfiguration config, bool production)
    {
        var connectionString = production ? ParseHerokuConnectionString(config["DATABASE_URL"]) : config["DefaultConnection"];
        services.AddDbContext<DataContext>(opt => opt.UseNpgsql(connectionString, npgsqlOpt => npgsqlOpt.EnableRetryOnFailure()));

        services.Scan(scan =>
        scan.FromCallingAssembly()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Queries")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static string ParseHerokuConnectionString(string connectionString)
    {
        connectionString = connectionString.Replace("postgres://", string.Empty);
        var pgUserPass = connectionString.Split("@")[0];
        var pgHostPortDb = connectionString.Split("@")[1];
        var pgHostPort = pgHostPortDb.Split("/")[0];
        var pgDb = pgHostPortDb.Split("/")[1];
        var pgUser = pgUserPass.Split(":")[0];
        var pgPass = pgUserPass.Split(":")[1];
        var pgHost = pgHostPort.Split(":")[0];
        var pgPort = pgHostPort.Split(":")[1];

        return $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
    }
}
