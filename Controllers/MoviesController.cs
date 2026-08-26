using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopMovies.Data;
using TopMovies.Models;

namespace TopMovies.Controllers;

public class MoviesController : Controller
{
    private readonly MovieDbContext _db;
    private readonly IWebHostEnvironment _env;

    public MoviesController(MovieDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _db.Movies.AsNoTracking().OrderBy(m => m.Id).ToListAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _db.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
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
        ValidatePoster(movie);

        if (!ModelState.IsValid) return View(movie);

        if (movie.PosterFile is not null)
        {
            movie.PosterPath = await SavePosterFileAsync(movie.PosterFile);
        }

        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _db.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Genre,ReleaseYear,PosterPath,Description,PosterFile")] Movie movie)
    {
        if (id != movie.Id) return NotFound();

        ValidatePoster(movie);

        if (!ModelState.IsValid) return View(movie);

        if (movie.PosterFile is not null)
        {
            movie.PosterPath = await SavePosterFileAsync(movie.PosterFile);
        }

        try
        {
            _db.Movies.Update(movie);
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Movies.AnyAsync(m => m.Id == id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var movie = await _db.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null) return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _db.Movies.FindAsync(id);
        if (movie is not null)
        {
            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private void ValidatePoster(Movie movie)
    {
        ModelState.Remove(nameof(Movie.PosterFile));

        if (movie.PosterFile is null && string.IsNullOrWhiteSpace(movie.PosterPath))
        {
            ModelState.AddModelError(nameof(Movie.PosterPath), "Завантажте файл постера або вкажіть шлях до зображення");
        }
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
