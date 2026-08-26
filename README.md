# TopMovies

Веб-додаток на ASP.NET Core MVC (.NET 9), який відображає добірку кращих фільмів
у вигляді стилізованої сітки з постерами.

## Дані

Дані про фільми зберігаються в оперативній пам'яті — список об'єктів моделі `Movie`
у сервісі `Services/MovieRepository.cs`, зареєстрованому як singleton-сервіс і
переданому в `HomeController` через Dependency Injection.

Для кожного фільму зберігається:

- назва
- режисер
- жанр
- рік випуску
- постер (шлях до зображення у `wwwroot/images/posters`)
- короткий опис

Усього в базі 8 фільмів.

## Запуск

```bash
dotnet run
```

Додаток буде доступний на `http://localhost:5159` (порт визначено в
`Properties/launchSettings.json`).

## Структура

- `Models/Movie.cs` — модель фільму
- `Services/MovieRepository.cs` — сховище фільмів у пам'яті
- `Controllers/HomeController.cs` — передає список фільмів у в'юху
- `Views/Home/Index.cshtml` — головна сторінка із сіткою фільмів
- `wwwroot/css/site.css` — стилізація сітки та карток
- `wwwroot/images/posters/` — постери фільмів (SVG)

## Скріншоти

![Головна сторінка](screenshots/home-hero.png)

![Повна сітка фільмів](screenshots/home-all.png)
