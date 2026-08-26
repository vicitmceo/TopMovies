using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using TopMovies.Validation;

namespace TopMovies.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Введіть назву фільму")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Назва має бути від 2 до 200 символів")]
    [Display(Name = "Назва")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введіть режисера")]
    [StringLength(150, ErrorMessage = "Ім'я режисера не може перевищувати 150 символів")]
    [Display(Name = "Режисер")]
    public string Director { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введіть жанр")]
    [StringLength(100, ErrorMessage = "Жанр не може перевищувати 100 символів")]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Вкажіть рік випуску")]
    [Range(1888, 2100, ErrorMessage = "Рік випуску має бути між 1888 та 2100")]
    [NotFutureYear]
    [Display(Name = "Рік випуску")]
    public int ReleaseYear { get; set; }

    [StringLength(300)]
    [PosterRequired]
    [Display(Name = "Шлях до постера")]
    public string PosterPath { get; set; } = string.Empty;

    [Required(ErrorMessage = "Додайте короткий опис")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Опис має бути від 10 до 1000 символів")]
    [Display(Name = "Опис")]
    public string Description { get; set; } = string.Empty;

    [NotMapped]
    [Display(Name = "Файл постера")]
    public IFormFile? PosterFile { get; set; }
}
