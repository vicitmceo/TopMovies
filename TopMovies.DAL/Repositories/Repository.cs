using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TopMovies.DAL.Interfaces;
using TopMovies.DAL.Persistence;

namespace TopMovies.DAL.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MovieDbContext Db;
    protected readonly DbSet<T> DbSet;

    public Repository(MovieDbContext db)
    {
        Db = db;
        DbSet = db.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await DbSet.FindAsync(id);

    public async Task<List<T>> GetAllAsync() => await DbSet.AsNoTracking().ToListAsync();

    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);

    public void Update(T entity) => Db.Entry(entity).State = EntityState.Modified;

    public void Delete(T entity) => DbSet.Remove(entity);

    public IQueryable<T> Query(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = DbSet.AsNoTracking();
        if (filter is not null) query = query.Where(filter);
        return query;
    }
}
