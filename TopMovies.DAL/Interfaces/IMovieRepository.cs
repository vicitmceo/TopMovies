using TopMovies.DAL.Entities;

namespace TopMovies.DAL.Interfaces;

public interface IMovieRepository : IRepository<Movie>
{
    Task<List<Movie>> SearchByTitleAsync(string title);
}
