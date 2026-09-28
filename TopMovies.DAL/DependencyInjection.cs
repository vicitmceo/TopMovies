using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TopMovies.DAL.Interfaces;
using TopMovies.DAL.Persistence;
using TopMovies.DAL.Repositories;

namespace TopMovies.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string? connection)
    {
        services.AddDbContext<MovieDbContext>(options => options.UseSqlite(connection));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
