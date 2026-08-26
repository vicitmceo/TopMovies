# TopMovies

Веб-додаток на ASP.NET Core MVC (.NET 9), який відображає добірку кращих фільмів
у вигляді стилізованої сітки з постерами.

## Дані

Дані про фільми зберігаються в **локальній базі даних SQL Server (LocalDB)** —
таблиця `Movies`, доступ через Entity Framework Core (`Data/MovieDbContext.cs`).
Початкові дані завантажуються через EF Core міграцію (`Migrations/`), а при
старті застосунку викликається `db.Database.Migrate()`, який автоматично
створює базу та застосовує міграції.

Для кожного фільму зберігається:

- назва
- режисер
- жанр
- рік випуску
- постер (шлях до зображення у `wwwroot/images/posters`)
- короткий опис

Усього в базі 8 фільмів.

## Запуск

Потрібен встановлений SQL Server LocalDB (входить до Visual Studio /
SQL Server Express LocalDB). Рядок підключення в `appsettings.json`:

```
Server=(localdb)\MSSQLLocalDB;Database=TopMoviesDb;Trusted_Connection=True;...
```

```bash
dotnet run
```

База даних та таблиця створюються автоматично при першому запуску.
Додаток буде доступний на `http://localhost:5159`.

## Структура

- `Models/Movie.cs` — модель фільму (Entity Framework сутність)
- `Data/MovieDbContext.cs` — DbContext, seed-дані через `HasData`
- `Migrations/` — EF Core міграції
- `Controllers/HomeController.cs` — читає фільми з БД через `MovieDbContext`
- `Views/Home/Index.cshtml` — головна сторінка із сіткою фільмів
- `wwwroot/css/site.css` — стилізація сітки та карток
- `wwwroot/images/posters/` — постери фільмів (SVG)

## Скріншоти

![Головна сторінка](screenshots/home-hero.png)

![Повна сітка фільмів](screenshots/home-all.png)
