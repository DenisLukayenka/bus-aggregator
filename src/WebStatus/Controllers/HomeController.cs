using Microsoft.AspNetCore.Mvc;

namespace WebStatus.Controllers;

public class HomeController : Controller
{
    private IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        var basePath = this._configuration["PATH_BASE"];
        return Redirect($"{basePath}/hc-ui");
    }

    [HttpGet("/Config")]
    public IActionResult Config()
    {
        var configurationValues = _configuration.GetSection("HealthChecksUI:HealthChecks")
            .GetChildren()
            .SelectMany(cs => cs.GetChildren())
            .Union(_configuration.GetSection("HealthChecks-UI:HealthChecks")
            .GetChildren()
            .SelectMany(cs => cs.GetChildren()))
            .ToDictionary(v => v.Path, v => v.Value);

        if (configurationValues is null)
        {
            configurationValues = new Dictionary<string, string>();
        }

        return View(configurationValues);
    }

    public IActionResult Error()
    {
        return View();
    }
}