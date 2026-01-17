namespace CupyVoiceAPI.Models.Dto;

public sealed record EpisodeDto(Guid Id, Guid SeasonId, int Number, string Title, int DurationSeconds, string? VideoUrl);