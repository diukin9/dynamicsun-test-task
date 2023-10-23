using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicSunTestTask.Application.ViewModels;

public class UploadWeatherDataViewModel
{
    public List<SelectListItem> CitiesSelectList { get; set; } = null!;
    public int SelectedCityId { get; set; }
    public IFormFileCollection Files { get; set; } = null!;
}
