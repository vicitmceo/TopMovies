using System.ComponentModel.DataAnnotations;

namespace TopMovies.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string PosterPath { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
