using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopMovies.DAL.Interfaces;
using TopMovies.Models;

namespace TopMovies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _unitOfWork.Movies.GetAllAsync();
        return View(movies.OrderBy(m => m.Id).ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("Home/NotFoundPage")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = 404;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
