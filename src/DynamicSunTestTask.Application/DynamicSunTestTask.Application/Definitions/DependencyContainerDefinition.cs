using System.Reflection;
using DynamicSunTestTask.Data.EntityFrameworkCore;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;
using DynamicSunTestTask.Infrastructure.Common.Attributes;
using DynamicSunTestTask.Infrastructure.Common.Extensions;

namespace DynamicSunTestTask.Application.Definitions;

[CallingOrder(6)]
public class DependencyContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(Assembly.GetAssembly(typeof(ApplicationDbContext))!);

        services.AddSingleton(configuration);
    }
}