using Microsoft.EntityFrameworkCore;
using TopMovies.Models;

namespace TopMovies.Data;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Title = "Втеча з Шоушенка",
                Director = "Френк Дарабонт",
                Genre = "Драма",
                ReleaseYear = 1994,
                PosterPath = "/images/posters/shawshank.svg",
                Description = "Банкір Енді Дюфрейн, засуджений за вбивство, яке не скоював, знаходить надію та дружбу за стінами в'язниці Шоушенк."
            },
            new Movie
            {
                Id = 2,
                Title = "Хрещений батько",
                Director = "Френсіс Форд Коппола",
                Genre = "Кримінал, драма",
                ReleaseYear = 1972,
                PosterPath = "/images/posters/godfather.svg",
                Description = "Історія родини Корлеоне та боротьби за владу в світі організованої злочинності Нью-Йорка."
            },
            new Movie
            {
                Id = 3,
                Title = "Темний лицар",
                Director = "Крістофер Нолан",
                Genre = "Бойовик, кримінал",
                ReleaseYear = 2008,
                PosterPath = "/images/posters/darkknight.svg",
                Description = "Бетмен протистоїть Джокеру, який занурює Готем у хаос та випробовує моральні межі героя."
            },
            new Movie
            {
                Id = 4,
                Title = "Кримінальне чтиво",
                Director = "Квентін Тарантіно",
                Genre = "Кримінал, драма",
                ReleaseYear = 1994,
                PosterPath = "/images/posters/pulpfiction.svg",
                Description = "Переплетені історії найманих вбивць, боксера та бандита у Лос-Анджелесі."
            },
            new Movie
            {
                Id = 5,
                Title = "Початок",
                Director = "Крістофер Нолан",
                Genre = "Фантастика, трилер",
                ReleaseYear = 2010,
                PosterPath = "/images/posters/inception.svg",
                Description = "Викрадач думок отримує завдання здійснити зворотну операцію — вкласти ідею у чужу підсвідомість."
            },
            new Movie
            {
                Id = 6,
                Title = "Форрест Гамп",
                Director = "Роберт Земекіс",
                Genre = "Драма, мелодрама",
                ReleaseYear = 1994,
                PosterPath = "/images/posters/forrestgump.svg",
                Description = "Історія простодушного чоловіка з Алабами, який став свідком та учасником ключових подій американської історії."
            },
            new Movie
            {
                Id = 7,
                Title = "Інтерстеллар",
                Director = "Крістофер Нолан",
                Genre = "Фантастика, драма",
                ReleaseYear = 2014,
                PosterPath = "/images/posters/interstellar.svg",
                Description = "Група дослідників вирушає крізь червоточину в пошуках нового дому для людства."
            },
            new Movie
            {
                Id = 8,
                Title = "Паразити",
                Director = "Пон Джун Хо",
                Genre = "Трилер, драма",
                ReleaseYear = 2019,
                PosterPath = "/images/posters/parasite.svg",
                Description = "Бідна родина поступово проникає у життя заможного сімейства, що призводить до непередбачуваних наслідків."
            }
        );
    }
}
