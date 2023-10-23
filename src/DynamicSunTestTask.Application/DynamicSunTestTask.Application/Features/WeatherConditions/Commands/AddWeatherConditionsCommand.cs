using DynamicSunTestTask.Application.DTOs;
using DynamicSunTestTask.Data.EntityFrameworkCore.Repositories.Interfaces;
using DynamicSunTestTask.Domain.RelationalDatabase.Enums;
using DynamicSunTestTask.Domain.RelationalDatabase.ValueObjects;
using DynamicSunTestTask.Infrastructure.Common.Extensions;
using DynamicSunTestTask.Infrastructure.Common.Helpers;
using DynamicSunTestTask.Infrastructure.Common.Models;
using DynamicSunTestTask.Parsers;
using DynamicSunTestTask.Parsers.Abstractions.Configuration;
using DynamicSunTestTask.Parsers.Abstractions.Parser;
using DynamicSunTestTask.Parsers.Models;
using MapsterMapper;
using MediatR;
using NPOI.SS.UserModel;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using static DynamicSunTestTask.Application.Constants;
using static DynamicSunTestTask.Infrastructure.Common.Constants;
using static DynamicSunTestTask.Infrastructure.Common.Exceptions;
using DTO = DynamicSunTestTask.Application.DTOs.WeatherConditionsDTO;
using Entites = DynamicSunTestTask.Domain.RelationalDatabase.Entities;
using Model = DynamicSunTestTask.Domain.RelationalDatabase.Entities.WeatherConditions;

namespace DynamicSunTestTask.Application.Features.WeatherConditions.Commands;

public class AddWeatherConditionsRequest : IRequest<InternalResponse>
{
    [Required] public int CityId { get; set; }

    [Required] public IFormFileCollection? Files { get; set; }
}

public class AddWeatherConditionsCommandHandler
    : IRequestHandler<AddWeatherConditionsRequest, InternalResponse>
{
    private readonly IMapper mapper;
    private readonly IWebHostEnvironment environment;
    private readonly IWeatherConditionsRepository wcRepository;
    private readonly ICityRepository cityRepository;
    private readonly ConcurrentQueue<Model> models = new();

    #region mapping configuration from excel file to entity

    private static IExcelParserConfiguration<DTO> Config { get; } = new ExcelParserConfiguration<DTO>(
        skipRows: new int[] { 0, 1, 2, 3 },
        mappings: new ExcelMapping<DTO>[]
        {
            new ((wc, row) => wc.LocalDate = row.GetCell(0).DateOnlyCellValue!.Value),
            new ((wc, row) => wc.LocalTime = row.GetCell(1).TimeOnlyCellValue!.Value),
            new ((wc, row) => wc.AirTemperature = row.GetCell(2).NumericCellValue!.Value),
            new ((wc, row) => wc.AirRelativeHumidityAsPercentage = (int) row.GetCell(3).NumericCellValue!.Value),
            new ((wc, row) => wc.DewPointInDegreesCelsius = row.GetCell(4).NumericCellValue!.Value),
            new ((wc, row) => wc.AtmosphericPressureInMmHg = (int) row.GetCell(5).NumericCellValue!.Value),
            new ((wc, row) => wc.WindDirection  = string.Join(separator: ", ",
                values: row.GetCell(6).EnumCellValues<WindDirection, int>(",") ?? Array.Empty<WindDirection>())),
            new ((wc, row) => wc.WindSpeedInMetersPerSecond = (int?) row.GetCell(7).NumericCellValue),
            new ((wc, row) => wc.CloudCoverAsPercentage = (int?) row.GetCell(8).NumericCellValue),
            new ((wc, row) => wc.LowerCloudLimitInMeters = (int?) row.GetCell(9).NumericCellValue),
            new ((wc, row) =>
            {
                wc.HorizontalVisibility = row.GetCell(10).CellType switch
                {
                    CellType.Numeric => new HorizontalVisibility((int) row.GetCell(10).NumericCellValue!)?.ToString(),
                    CellType.String => HorizontalVisibility.FromString(row.GetCell(10).StringCellValue)?.ToString(),
                    CellType.Blank => null!,
                    _ => throw new NotImplementedException()
                };
            }),
            new ((wc, row) => wc.WeatherPhenomena = row.GetCell(11).StringCellValue)
        });

    #endregion

    public AddWeatherConditionsCommandHandler(
        IWeatherConditionsRepository wcRepository,
        ICityRepository cityRepository,
        IWebHostEnvironment environment,
        IMapper mapper)
    {
        this.mapper = mapper;
        this.wcRepository = wcRepository;
        this.cityRepository = cityRepository;
        this.environment = environment;
    }

    public async Task<InternalResponse> Handle(
        AddWeatherConditionsRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalResponse();

        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.Failure(validation);

        var city = await cityRepository.FindByIdAsync(request.CityId, true, cancellationToken);
        if (city is null) return operation.Failure(ObjectNotFound(nameof(City)));

        var extensions = request.Files?.Select(x => $".{x.FileName.Split('.').Last()}").ToList();
        if (extensions?.Any(x => !EXCEL_FILE_EXTENSIONS.Contains(x.ToLower())) ?? false)
        {
            return operation.Failure(InvalidFileExtension);
        }

        var tasks = ProcessFiles(request.Files, city);

        while (!tasks.All(x => x.IsCompleted))
        {
            models.TryDequeue(out var item);
            if (item is not null) await wcRepository.AddAsync(item, cancellationToken);
        }

        await wcRepository.SaveChangesAsync(cancellationToken);

        return operation.Success();
    }

    private List<Task> ProcessFiles(IFormFileCollection? files, Entites.City city)
    {
        return files?.AsParallel().Select(async (file) =>
        {
            var fileName = $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss}_{file.FileName}";
            var dirPath = $"{environment.WebRootPath}/{PathToDirWithArchivesOfWeatherConditions}";
            await FileHelper.LoadAsync(dirPath, file, fileName);

            IParser parser = new ExcelParser(Path.Combine(dirPath, fileName));

            foreach (var item in parser.Parse(Config))
            {
                item.City = mapper.Map<Entites.City, CityDTO>(city);
                var entity = mapper.Map<DTO, Model>(item);
                entity.FileName = fileName;
                models.Enqueue(entity);
            }
        }).ToList() ?? new List<Task>();
    }
}
