using DynamicSunTestTask.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicSunTestTask.Application.ViewModels;

public class ViewDataArchiveViewModel
{
    public PageViewModel PageViewModel { get; set; } = null!;

    public int? Year { get; set; }
    public int? Month { get; set; }

    public List<WeatherConditionsDTO> WeatherConditions { get; set; } = null!;

}
