using Microsoft.AspNetCore.Mvc;

namespace SwarmMvc.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var instanceInfo = await GetInstanceInfo();
        ViewBag.InstanceInfo = instanceInfo;
        return View();
    }

    private async Task<string> GetInstanceInfo()
    {
        try
        {
            // Try to get EC2 instance metadata
            var response = await _httpClient.GetStringAsync("http://169.254.169.254/latest/meta-data/instance-id");
            return $"Instance ID: {response}";
        }
        catch
        {
            // Fallback if not running on EC2 or metadata service unavailable
            return $"Hostname: {Environment.MachineName}";
        }
    }
}