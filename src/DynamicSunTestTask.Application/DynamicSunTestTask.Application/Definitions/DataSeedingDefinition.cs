using DynamicSunTestTask.Data.EntityFrameworkCore;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;
using DynamicSunTestTask.Infrastructure.Common.Attributes;

namespace DynamicSunTestTask.Application.Definitions;

[CallingOrder(5)]
public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        DatabaseInitializer.Run(app.Services);
    }
}
