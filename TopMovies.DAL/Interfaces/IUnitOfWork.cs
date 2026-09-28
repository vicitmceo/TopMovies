namespace TopMovies.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IMovieRepository Movies { get; }

    Task<int> SaveChangesAsync();
}
