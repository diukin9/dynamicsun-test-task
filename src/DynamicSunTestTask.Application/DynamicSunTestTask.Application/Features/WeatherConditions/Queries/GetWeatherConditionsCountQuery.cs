using DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;
using DynamicSunTestTask.Infrastructure.Common.Extensions;
using DynamicSunTestTask.Infrastructure.Common.Helpers;
using DynamicSunTestTask.Infrastructure.Common.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DynamicSunTestTask.Application.Features.WeatherConditions.Queries;

public class GetWeatherConditionsCountRequest : IRequest<InternalResponse<int>>
{
    [Range(1, 12)]
    public int? Month { get; set; }

    public int? Year { get; set; }
}

public class GetWeatherConditionsCountQueryHandler
    : IRequestHandler<GetWeatherConditionsCountRequest, InternalResponse<int>>
{
    private readonly IWeatherConditionsRepository repository;

    public GetWeatherConditionsCountQueryHandler(IWeatherConditionsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<InternalResponse<int>> Handle(
        GetWeatherConditionsCountRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalResponse<int>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.Failure(validation);

        var count = await repository.CountAsync(
            filter: (e) => (!request.Month.HasValue 
                    || e.DateTimeUTC.AddHours(e.City.TimeZoneRelativeToUTC).Month == request.Month.Value)
                && (!request.Year.HasValue 
                    || e.DateTimeUTC.AddHours(e.City.TimeZoneRelativeToUTC).Year == request.Year.Value),
            cancellationToken: cancellationToken);

        return operation.Success(count);
    }
}
