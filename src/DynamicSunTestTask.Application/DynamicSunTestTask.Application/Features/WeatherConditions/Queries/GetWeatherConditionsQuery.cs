using DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;
using DynamicSunTestTask.Infrastructure.Common.Extensions;
using DynamicSunTestTask.Infrastructure.Common.Helpers;
using DynamicSunTestTask.Infrastructure.Common.Models;
using MapsterMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using DTO = DynamicSunTestTask.Application.DTOs.WeatherConditionsDTO;

namespace DynamicSunTestTask.Application.Features.WeatherConditions.Queries;

public class GetWeatherConditionsRequest : IRequest<InternalResponse<List<DTO>>>
{
    [Required, Range(1, 100)]
    public int PerPage { get; set; } = 15;

    [Required, Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 12)] 
    public int? Month {  get; set; }

    public int? Year { get; set; }
}

public class GetWeatherConditionsQueryHandler 
    : IRequestHandler<GetWeatherConditionsRequest, InternalResponse<List<DTO>>>
{
    private readonly IMapper mapper;
    private readonly IWeatherConditionsRepository repository;

    public GetWeatherConditionsQueryHandler(IMapper mapper, IWeatherConditionsRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<InternalResponse<List<DTO>>> Handle(
        GetWeatherConditionsRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalResponse<List<DTO>>();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.Failure(validation);

        var conditions = await repository.FindAsync(
            filter: (e) => (!request.Month.HasValue
                    || e.DateTimeUTC.AddHours(e.City.TimeZoneRelativeToUTC).Month == request.Month.Value)
                && (!request.Year.HasValue
                    || e.DateTimeUTC.AddHours(e.City.TimeZoneRelativeToUTC).Year == request.Year.Value),
            orderBy: (e) => e.DateTimeUTC,
            isAscending: true,
            skip: request.PerPage * (request.Page - 1),
            limit: request.PerPage,
            isTracked: false,
            cancellationToken: cancellationToken);

        var dtos = mapper.Map<List<DTO>>(conditions);

        return operation.Success(dtos);
    }
}
