using System.ComponentModel.DataAnnotations;

namespace CupyVoiceAPI.Models.Requests;

public sealed record class UpdateSeriesRequest(
    string Title,
    string? Description,
    string? PosterUrl
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (string.IsNullOrWhiteSpace(Title))
            yield return new ValidationResult("Title is required.", new[] { nameof(Title) });

        var t = Title?.Trim() ?? string.Empty;
        if (t.Length is < 1 or > 200)
            yield return new ValidationResult("Title length must be 1..200.", new[] { nameof(Title) });

        if (Description is not null && Description.Length > 2000)
            yield return new ValidationResult("Description max length is 2000.", new[] { nameof(Description) });

        if (PosterUrl is not null && PosterUrl.Length > 2000)
            yield return new ValidationResult("PosterUrl max length is 2000.", new[] { nameof(PosterUrl) });
    }
}
