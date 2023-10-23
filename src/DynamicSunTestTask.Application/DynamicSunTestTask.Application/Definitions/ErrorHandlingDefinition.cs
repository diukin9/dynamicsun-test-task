using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Text.Json;
using DynamicSunTestTask.Infrastructure.Common.AppDefinition;
using DynamicSunTestTask.Infrastructure.Common.Attributes;
using DynamicSunTestTask.Infrastructure.Common.Models;

namespace DynamicSunTestTask.Application.Definitions;

[CallingOrder(2)]
public class ErrorHandlingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(error => error.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature is not null)
            {
                Log.Error($"Something went wrong in the {contextFeature.Error}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                string message;

                if (env.IsDevelopment()) message = $"INTERNAL SERVER ERROR: {contextFeature.Error}";
                else message = "INTERNAL SERVER ERROR. PLEASE TRY AGAIN LATER";

                var json = JsonSerializer.Serialize(new FailureResponse(message));
                await context.Response.WriteAsync(json);
            }
        }));
    }
}
