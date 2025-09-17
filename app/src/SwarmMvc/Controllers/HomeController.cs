using Microsoft.AspNetCore.Mvc;

namespace SwarmMvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}