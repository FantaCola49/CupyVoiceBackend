using System.ComponentModel.DataAnnotations;

namespace CupyVoiceAPI.Models.Requests;

public sealed record class UpsertProgressRequest(int PositionSeconds) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (PositionSeconds < 0)
            yield return new ValidationResult("PositionSeconds must be >= 0.", new[] { nameof(PositionSeconds) });
    }
}
