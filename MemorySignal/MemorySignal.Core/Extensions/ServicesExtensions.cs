using MemorySignal.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemorySignal.Core.Extensions;
public static class ServicesExtensions
{
    public static void AddCoreServices(this IServiceCollection services, IConfiguration config, bool production)
    {
        //var connectionString = production ? ParseHerokuConnectionString(config["DATABASE_URL"]) : config["DefaultConnection"];
        //services.AddDbContext<DataContext>(opt => opt.UseNpgsql(connectionString, npgsqlOpt => npgsqlOpt.EnableRetryOnFailure()));
        var connectionString = "Data Source=App.db";
        services.AddDbContext<DataContext>(opt => opt.UseSqlite(connectionString));
        services.Configure<ImageApiOptions>(config.GetSection(nameof(ImageApiOptions)));
        services.AddAutoScanning();
    }

    private static void AddAutoScanning(this IServiceCollection services)
    {
        const string manager = "Manager";
        const string gameManager = "Game" + manager;
        services.Scan(scan =>
        scan.FromCallingAssembly()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Queries")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(c => c.Where(type => !type.Name.EndsWith(gameManager) && type.Name.EndsWith(manager)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(classes => classes.Where(type => type.Name.EndsWith(gameManager)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IAction<,>))
            .Where(c => !c.IsAbstract && !c.IsInterface))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            );
    }

    //private static string ParseHerokuConnectionString(string connectionString)
    //{
    //    connectionString = connectionString.Replace("postgres://", string.Empty);
    //    var pgUserPass = connectionString.Split("@")[0];
    //    var pgHostPortDb = connectionString.Split("@")[1];
    //    var pgHostPort = pgHostPortDb.Split("/")[0];
    //    var pgDb = pgHostPortDb.Split("/")[1];
    //    var pgUser = pgUserPass.Split(":")[0];
    //    var pgPass = pgUserPass.Split(":")[1];
    //    var pgHost = pgHostPort.Split(":")[0];
    //    var pgPort = pgHostPort.Split(":")[1];

    //    return $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
    //}
}
