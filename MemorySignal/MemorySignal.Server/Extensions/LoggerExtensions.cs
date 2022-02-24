using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace MemorySignal.Server.Extensions;

public static class LoggerExtensions
{
    public static void Configure(this Serilog.ILogger logger)
    {
        logger = new LoggerConfiguration()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .Enrich.FromLogContext()
            .CreateLogger();
    }
}