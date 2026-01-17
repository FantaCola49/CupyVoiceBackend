namespace CupyVoiceAPI.Models.Responses;

public sealed record SeriesListItemDto(Guid Id, string Title, string? Description, string? PosterUrl);
