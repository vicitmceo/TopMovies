using Microsoft.EntityFrameworkCore;
using TopMovies.DAL.Entities;
using TopMovies.DAL.Interfaces;
using TopMovies.DAL.Persistence;

namespace TopMovies.DAL.Repositories;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(MovieDbContext db) : base(db)
    {
    }

    public async Task<List<Movie>> SearchByTitleAsync(string title)
    {
        return await DbSet.AsNoTracking()
            .Where(m => EF.Functions.Like(m.Title, $"%{title}%"))
            .OrderBy(m => m.Id)
            .ToListAsync();
    }
}
