using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopMovies.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Genre", "PosterPath", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "Банкір Енді Дюфрейн, засуджений за вбивство, яке не скоював, знаходить надію та дружбу за стінами в'язниці Шоушенк.", "Френк Дарабонт", "Драма", "/images/posters/shawshank.svg", 1994, "Втеча з Шоушенка" },
                    { 2, "Історія родини Корлеоне та боротьби за владу в світі організованої злочинності Нью-Йорка.", "Френсіс Форд Коппола", "Кримінал, драма", "/images/posters/godfather.svg", 1972, "Хрещений батько" },
                    { 3, "Бетмен протистоїть Джокеру, який занурює Готем у хаос та випробовує моральні межі героя.", "Крістофер Нолан", "Бойовик, кримінал", "/images/posters/darkknight.svg", 2008, "Темний лицар" },
                    { 4, "Переплетені історії найманих вбивць, боксера та бандита у Лос-Анджелесі.", "Квентін Тарантіно", "Кримінал, драма", "/images/posters/pulpfiction.svg", 1994, "Кримінальне чтиво" },
                    { 5, "Викрадач думок отримує завдання здійснити зворотну операцію — вкласти ідею у чужу підсвідомість.", "Крістофер Нолан", "Фантастика, трилер", "/images/posters/inception.svg", 2010, "Початок" },
                    { 6, "Історія простодушного чоловіка з Алабами, який став свідком та учасником ключових подій американської історії.", "Роберт Земекіс", "Драма, мелодрама", "/images/posters/forrestgump.svg", 1994, "Форрест Гамп" },
                    { 7, "Група дослідників вирушає крізь червоточину в пошуках нового дому для людства.", "Крістофер Нолан", "Фантастика, драма", "/images/posters/interstellar.svg", 2014, "Інтерстеллар" },
                    { 8, "Бідна родина поступово проникає у життя заможного сімейства, що призводить до непередбачуваних наслідків.", "Пон Джун Хо", "Трилер, драма", "/images/posters/parasite.svg", 2019, "Паразити" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
