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
            // Try to get EC2 instance metadata with timeout
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            var instanceId = await _httpClient.GetStringAsync("http://169.254.169.254/latest/meta-data/instance-id", cts.Token);

            // Get container hostname for uniqueness within the same instance
            var hostname = Environment.MachineName;
            var containerInfo = hostname.Length > 12 ? hostname.Substring(0, 12) : hostname;

            return $"{instanceId}-{containerInfo}";
        }
        catch
        {
            // Fallback if not running on EC2 - use full container hostname
            var hostname = Environment.MachineName;
            return hostname;
        }
    }
}