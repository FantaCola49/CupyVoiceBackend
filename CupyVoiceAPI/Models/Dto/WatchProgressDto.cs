namespace CupyVoiceAPI.Models.Dto;

public sealed record WatchProgressDto(Guid UserId, Guid EpisodeId, int PositionSeconds, DateTime UpdatedAtUtc);
