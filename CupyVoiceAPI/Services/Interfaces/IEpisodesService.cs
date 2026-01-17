using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Requests;

namespace CupyVoiceAPI.Services.Interfaces;


/// <summary>
/// Сервис управления эпизодами.
/// </summary>
public interface IEpisodesService
{
    /// <summary>
    /// Получить эпизоды сезона.
    /// </summary>
    /// <param name="seasonId">Идентификатор сезона.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<IReadOnlyList<EpisodeDto>> GetBySeasonAsync(Guid seasonId, CancellationToken ct);

    /// <summary>
    /// Создать эпизод.
    /// </summary>
    /// <param name="seasonId">Id сезона.</param>
    /// <param name="req">Данные для создания эпизода.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<EpisodeDto> CreateAsync(Guid seasonId, CreateEpisodeRequest req, CancellationToken ct);

    /// <summary>
    /// Обновить эпизод.
    /// </summary>
    /// <param name="episodeId">Идентификатор эпизода.</param>
    /// <param name="req">Данные для обновления.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<EpisodeDto> UpdateAsync(Guid episodeId, UpdateEpisodeRequest req, CancellationToken ct);

    /// <summary>
    /// Получить параметры воспроизведения эпизода (MVP: mp4 url).
    /// </summary>
    /// <param name="episodeId">Идентификатор эпизода.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<PlaybackDto> GetPlaybackAsync(Guid episodeId, CancellationToken ct);

    /// <summary>
    /// Удалить эпизод.
    /// </summary>
    /// <param name="episodeId">Идентификатор эпизода.</param>
    /// <param name="ct">Токен отмены.</param>
    Task DeleteAsync(Guid episodeId, CancellationToken ct);
}
