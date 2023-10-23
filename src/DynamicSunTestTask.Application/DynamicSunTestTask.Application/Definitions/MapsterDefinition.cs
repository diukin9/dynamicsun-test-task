using Mapster;
using MapsterMapper;
using System.Reflection;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;
using DynamicSunTestTask.Infrastructure.Common.Attributes;

namespace DynamicSunTestTask.Application.Definitions;

[CallingOrder(3)]
public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly()!);
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}