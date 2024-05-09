using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto1.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

 public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Ejercicios()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
