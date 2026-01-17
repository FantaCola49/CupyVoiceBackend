using System.ComponentModel.DataAnnotations;

namespace CupyVoiceAPI.Models.Requests;

public sealed record class CreateEpisodeRequest(
    int Number,
    string Title,
    int DurationSeconds,
    string? VideoUrl
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (Number <= 0)
            yield return new ValidationResult("Episode number must be > 0.", new[] { nameof(Number) });

        if (string.IsNullOrWhiteSpace(Title))
            yield return new ValidationResult("Title is required.", new[] { nameof(Title) });

        var t = Title?.Trim() ?? string.Empty;
        if (t.Length is < 1 or > 300)
            yield return new ValidationResult("Title length must be 1..300.", new[] { nameof(Title) });

        if (DurationSeconds <= 0)
            yield return new ValidationResult("DurationSeconds must be > 0.", new[] { nameof(DurationSeconds) });

        if (VideoUrl is not null && VideoUrl.Length > 4000)
            yield return new ValidationResult("VideoUrl max length is 4000.", new[] { nameof(VideoUrl) });
    }
}
