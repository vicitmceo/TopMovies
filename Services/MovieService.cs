using Microsoft.EntityFrameworkCore;
using TopMovies.Data;
using TopMovies.Models;

namespace TopMovies.Services;

public class MovieService : IMovieService
{
    private readonly MovieDbContext _db;
    private readonly IWebHostEnvironment _env;

    public MovieService(MovieDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _db.Movies.AsNoTracking().OrderBy(m => m.Id).ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _db.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task CreateAsync(Movie movie)
    {
        if (movie.PosterFile is not null)
        {
            movie.PosterPath = await SavePosterFileAsync(movie.PosterFile);
        }

        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, Movie movie)
    {
        var existing = await _db.Movies.FindAsync(id);
        if (existing is null) return false;

        if (movie.PosterFile is not null)
        {
            movie.PosterPath = await SavePosterFileAsync(movie.PosterFile);
        }
        else
        {
            movie.PosterPath = existing.PosterPath;
        }

        _db.Entry(existing).CurrentValues.SetValues(movie);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _db.Movies.FindAsync(id);
        if (movie is null) return false;

        _db.Movies.Remove(movie);
        await _db.SaveChangesAsync();
        return true;
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
