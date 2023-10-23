using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json.Serialization;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;
using DynamicSunTestTask.Infrastructure.Common.Attributes;

namespace DynamicSunTestTask.Application.Definitions;

[CallingOrder(0)]
public class InitialCommonDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
    }
}

[CallingOrder(7)]
public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddLocalization();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
        services.AddMemoryCache();
        services.AddControllersWithViews();
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHsts();
        app.UseHttpsRedirection();
        app.MapDefaultControllerRoute();
        app.UseResponseCaching();
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
    }
}