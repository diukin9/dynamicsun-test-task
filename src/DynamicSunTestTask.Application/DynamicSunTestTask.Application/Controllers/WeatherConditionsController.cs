using DynamicSunTestTask.Application.Features.City.Queries;
using DynamicSunTestTask.Application.Features.WeatherConditions.Commands;
using DynamicSunTestTask.Application.Features.WeatherConditions.Queries;
using DynamicSunTestTask.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicSunTestTask.Application.Controllers;

public class WeatherConditionsController : Controller
{
    private readonly IMediator mediator;

    public WeatherConditionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> UploadWeatherData(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetCitiesRequest(), cancellationToken);

        var model = new UploadWeatherDataViewModel()
        {
            CitiesSelectList = response?.Data?
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
                .ToList() ?? new List<SelectListItem>(),
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UploadWeatherData(
        UploadWeatherDataViewModel model, 
        CancellationToken cancellationToken)
    {
        var request = new AddWeatherConditionsRequest() 
        { 
            CityId = model.SelectedCityId, 
            Files = model.Files 
        };

        var response = await mediator.Send(request, cancellationToken);

        TempData["isSuccess"] = response.IsSuccess;
        TempData["errorMessage"] = response.Exception?.Message;

        return RedirectToAction(nameof(UploadWeatherData));
    }

    [HttpGet]
    public async Task<IActionResult> ViewDataArchive(int page = 1, int? month = null, int? year = null)
    {
        var model = new ViewDataArchiveViewModel() { Month = month, Year = year };

        var countRequest = new GetWeatherConditionsCountRequest() { Month = month, Year = year };
        var conditionCount = await mediator.Send(countRequest);

        model.PageViewModel = new PageViewModel(conditionCount.Data, page, 100);

        var conditionsRequest = new GetWeatherConditionsRequest()
        { Month = month, Year = year, Page = page, PerPage = 100 };
        var conditions = await mediator.Send(conditionsRequest);

        model.WeatherConditions = conditions.Data ?? new List<DTOs.WeatherConditionsDTO>();

        return View(model);
    }
}
