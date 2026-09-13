using Microsoft.AspNetCore.Mvc;
using TopMovies.Models;
using TopMovies.Services;

namespace TopMovies.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _movieService.GetAllAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _movieService.GetByIdAsync(id.Value);
        if (movie is null) return NotFound();

        return View(movie);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Director,Genre,ReleaseYear,PosterPath,Description,PosterFile")] Movie movie)
    {
        if (!ModelState.IsValid) return View(movie);

        await _movieService.CreateAsync(movie);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _movieService.GetByIdAsync(id.Value);
        if (movie is null) return NotFound();

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Genre,ReleaseYear,PosterPath,Description,PosterFile")] Movie movie)
    {
        if (id != movie.Id) return NotFound();

        if (!ModelState.IsValid) return View(movie);

        var updated = await _movieService.UpdateAsync(id, movie);
        if (!updated) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _movieService.GetByIdAsync(id.Value);
        if (movie is null) return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _movieService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
