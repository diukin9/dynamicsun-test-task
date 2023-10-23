using DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;
using DynamicSunTestTask.Infrastructure.Common.Models;
using MapsterMapper;
using MediatR;
using DTO = DynamicSunTestTask.Application.DTOs.CityDTO;

namespace DynamicSunTestTask.Application.Features.City.Queries;

public class GetCitiesRequest : IRequest<InternalResponse<List<DTO>>> { }

public class GetCitiesQueryHandler
    : IRequestHandler<GetCitiesRequest, InternalResponse<List<DTO>>>
{
    private readonly IMapper mapper;
    private readonly ICityRepository repository;

    public GetCitiesQueryHandler(IMapper mapper, ICityRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<InternalResponse<List<DTO>>> Handle(
        GetCitiesRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalResponse<List<DTO>>();

        var conditions = await repository.AllAsync(
            orderBy: (e) => e.Name,
            isAscending: true,
            isTracked: false,
            cancellationToken: cancellationToken);

        var dtos = mapper.Map<List<DTO>>(conditions);

        return operation.Success(dtos);
    }
}