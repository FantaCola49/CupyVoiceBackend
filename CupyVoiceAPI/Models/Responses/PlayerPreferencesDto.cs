namespace CupyVoiceAPI.Models.Responses;

public sealed record PlayerPreferencesDto(Guid UserId, string? PreferredQuality, double PlaybackRate);
