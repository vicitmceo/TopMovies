using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TopMovies.Models;
using TopMovies.Services;

namespace TopMovies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MovieRepository _movieRepository;

    public HomeController(ILogger<HomeController> logger, MovieRepository movieRepository)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    public IActionResult Index()
    {
        var movies = _movieRepository.GetAll();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
