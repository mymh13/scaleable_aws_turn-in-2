using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using SwarmMvc.Models;

namespace SwarmMvc.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IDynamoDBContext _dynamoContext;

    public HomeController(HttpClient httpClient, IDynamoDBContext dynamoContext)
    {
        _httpClient = httpClient;
        _dynamoContext = dynamoContext;
    }

    public async Task<IActionResult> Index()
    {
        var instanceInfo = await GetInstanceInfo();
        ViewBag.InstanceInfo = instanceInfo;

        // Get recent timestamps from DynamoDB
        var timestamps = await GetRecentTimestamps();
        ViewBag.Timestamps = timestamps;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RecordTimestamp()
    {
        try
        {
            var instanceInfo = await GetInstanceInfo();

            var record = new TimestampRecord
            {
                Timestamp = DateTime.UtcNow,
                ContainerInfo = instanceInfo
            };

            await _dynamoContext.SaveAsync(record);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Add error to ViewBag for debugging
            ViewBag.Error = $"Error recording timestamp: {ex.Message}";
            ViewBag.InstanceInfo = await GetInstanceInfo();
            ViewBag.Timestamps = await GetRecentTimestamps();
            return View("Index");
        }
    }

    private async Task<string> GetInstanceInfo()
    {
        try
        {
            // Try to get EC2 instance metadata with timeout
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            var instanceId = await _httpClient.GetStringAsync("http://169.254.169.254/latest/meta-data/instance-id", cts.Token);

            // Get full container hostname for uniqueness
            var hostname = Environment.MachineName;

            return $"{instanceId} | Container: {hostname}";
        }
        catch
        {
            // Fallback if not running on EC2 - use full container hostname
            var hostname = Environment.MachineName;
            return $"Container: {hostname}";
        }
    }

    private async Task<List<TimestampRecord>> GetRecentTimestamps()
    {
        try
        {
            var search = _dynamoContext.ScanAsync<TimestampRecord>(null);
            var results = await search.GetRemainingAsync();
            return results.OrderByDescending(x => x.Timestamp).Take(5).ToList();
        }
        catch
        {
            return new List<TimestampRecord>();
        }
    }
}