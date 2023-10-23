using Microsoft.EntityFrameworkCore;
using DynamicSunTestTask.Data.EntityFrameworkCore;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;
using DynamicSunTestTask.Infrastructure.Common.Attributes;

namespace DynamicSunTestTask.Application.Definitions;

[CallingOrder(1)]
public class DatabasesDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration["ConnectionStrings:PostgreSQL"]);
            config.EnableSensitiveDataLogging();
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
