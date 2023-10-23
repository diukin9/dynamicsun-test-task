using Serilog.Events;
using Serilog;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // configure logger (Serilog)
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    builder.Host.UseSerilog();

    // adding definitions for application
    builder.Services.AddDefinitions(builder, typeof(Program));

    // create application
    var app = builder.Build();

    // using definition for application
    app.UseDefinitions();

    // start application
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}