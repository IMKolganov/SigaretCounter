using Microsoft.AspNetCore.Mvc;

namespace SigaretCounter.Controllers;

public class HealthController : Controller
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    public string Index()
    {
        return "I am ok.";
    }
}