using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TopMovies.Validation;

public class NotFutureYearAttribute : ValidationAttribute, IClientModelValidator
{
    public NotFutureYearAttribute()
    {
        ErrorMessage = "Рік випуску не може бути пізніше поточного року";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int year && year > DateTime.Now.Year)
        {
            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName ?? string.Empty });
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val")) context.Attributes.Add("data-val", "true");
        if (!context.Attributes.ContainsKey("data-val-notfutureyear")) context.Attributes.Add("data-val-notfutureyear", ErrorMessage ?? string.Empty);
        if (!context.Attributes.ContainsKey("data-val-notfutureyear-max")) context.Attributes.Add("data-val-notfutureyear-max", DateTime.Now.Year.ToString());
    }
}
