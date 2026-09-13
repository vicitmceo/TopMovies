using TopMovies.Models;

namespace TopMovies.Services;

public interface IMovieService
{
    Task<List<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task CreateAsync(Movie movie);
    Task<bool> UpdateAsync(int id, Movie movie);
    Task<bool> DeleteAsync(int id);
}
