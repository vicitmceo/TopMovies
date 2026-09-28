using Microsoft.AspNetCore.Mvc;
using TopMovies.DAL.Entities;
using TopMovies.DAL.Interfaces;

namespace TopMovies.Controllers;

public class MoviesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env;

    public MoviesController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _unitOfWork.Movies.GetAllAsync();
        return View(movies.OrderBy(m => m.Id).ToList());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _unitOfWork.Movies.GetByIdAsync(id.Value);
        if (movie is null) return NotFound();

        return View(movie);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Director,Genre,ReleaseYear,PosterPath,Description")] Movie movie, IFormFile? posterFile)
    {
        if (!ModelState.IsValid) return View(movie);

        if (posterFile is not null)
        {
            movie.PosterPath = await SavePosterFileAsync(posterFile);
        }

        await _unitOfWork.Movies.AddAsync(movie);
        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _unitOfWork.Movies.GetByIdAsync(id.Value);
        if (movie is null) return NotFound();

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Genre,ReleaseYear,PosterPath,Description")] Movie movie, IFormFile? posterFile)
    {
        if (id != movie.Id) return NotFound();

        if (!ModelState.IsValid) return View(movie);

        var existing = await _unitOfWork.Movies.GetByIdAsync(id);
        if (existing is null) return NotFound();

        movie.PosterPath = posterFile is not null
            ? await SavePosterFileAsync(posterFile)
            : existing.PosterPath;

        _unitOfWork.Movies.Update(movie);
        await _unitOfWork.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _unitOfWork.Movies.GetByIdAsync(id.Value);
        if (movie is null) return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id);
        if (movie is not null)
        {
            _unitOfWork.Movies.Delete(movie);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<string> SavePosterFileAsync(IFormFile file)
    {
        var uploadsDir = Path.Combine(_env.WebRootPath, "images", "posters", "uploads");
        Directory.CreateDirectory(uploadsDir);

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsDir, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/images/posters/uploads/{fileName}";
    }
}
