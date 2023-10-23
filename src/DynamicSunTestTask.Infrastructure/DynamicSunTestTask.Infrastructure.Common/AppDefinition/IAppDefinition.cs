using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicSunTestTask.Infrastructure.Common.AppDefinition;

internal interface IAppDefinition
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    void ConfigureApplication(WebApplication app, IWebHostEnvironment env);
}