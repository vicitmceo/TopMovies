using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TopMovies.Models;

namespace TopMovies.Validation;

public class PosterRequiredAttribute : ValidationAttribute, IClientModelValidator
{
    public PosterRequiredAttribute()
    {
        ErrorMessage = "Завантажте файл постера або вкажіть шлях до зображення";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var movie = (Movie)validationContext.ObjectInstance;
        var posterPath = value as string;

        if (string.IsNullOrWhiteSpace(posterPath) && movie.PosterFile is null)
        {
            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName ?? nameof(Movie.PosterPath) });
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val")) context.Attributes.Add("data-val", "true");
        if (!context.Attributes.ContainsKey("data-val-posterrequired")) context.Attributes.Add("data-val-posterrequired", ErrorMessage ?? string.Empty);
    }
}
