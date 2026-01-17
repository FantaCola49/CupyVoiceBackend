using System.ComponentModel.DataAnnotations;

namespace CupyVoiceAPI.Models.Requests;

public sealed record class UpdatePlayerPreferencesRequest(
    string? PreferredQuality,
    double PlaybackRate
) : IValidatableObject
{
    private static readonly HashSet<string> AllowedQualities = new(StringComparer.OrdinalIgnoreCase)
    {
        "auto", "240p", "360p", "480p", "720p", "1080p", "1440p", "2160p"
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (PlaybackRate is < 0.25 or > 3.0)
            yield return new ValidationResult("PlaybackRate must be in range 0.25..3.0.", new[] { nameof(PlaybackRate) });

        if (PreferredQuality is null) yield break;

        var q = PreferredQuality.Trim();
        if (q.Length == 0) yield break;

        if (!AllowedQualities.Contains(q))
            yield return new ValidationResult("PreferredQuality is not supported.", new[] { nameof(PreferredQuality) });
    }
}
