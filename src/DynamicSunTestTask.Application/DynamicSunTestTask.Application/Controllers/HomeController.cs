using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTestTask.Application.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
