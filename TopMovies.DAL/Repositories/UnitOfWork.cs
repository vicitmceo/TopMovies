using TopMovies.DAL.Interfaces;
using TopMovies.DAL.Persistence;

namespace TopMovies.DAL.Repositories;

// Unit of Work тримає один спільний DbContext для всіх репозиторіїв
// і відповідає за єдину точку збереження змін (SaveChangesAsync).
public class UnitOfWork : IUnitOfWork
{
    private readonly MovieDbContext _db;
    private IMovieRepository? _movies;

    public UnitOfWork(MovieDbContext db)
    {
        _db = db;
    }

    public IMovieRepository Movies => _movies ??= new MovieRepository(_db);

    public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();

    public void Dispose() => _db.Dispose();
}
