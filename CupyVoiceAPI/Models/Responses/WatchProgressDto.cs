namespace CupyVoiceAPI.Models.Responses;

public sealed record WatchProgressDto(Guid UserId, Guid EpisodeId, int PositionSeconds, DateTime UpdatedAtUtc);
