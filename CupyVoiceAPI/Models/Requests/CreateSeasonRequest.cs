using System.ComponentModel.DataAnnotations;

namespace CupyVoiceAPI.Models.Requests;

public sealed record class CreateSeasonRequest(int Number) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext _)
    {
        if (Number <= 0)
            yield return new ValidationResult("Season number must be > 0.", new[] { nameof(Number) });
    }
}
