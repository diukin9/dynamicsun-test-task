using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicSunTestTask.Infrastructure.Common.AppDefinition;

public abstract class AppDefinition : IAppDefinition
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

    public virtual void ConfigureApplication(WebApplication app, IWebHostEnvironment env) { }
}