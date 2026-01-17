using CupyVoiceAPI.Models.Dto;

namespace CupyVoiceAPI.Services.Interfaces;

/// <summary>
/// Сервис прогресса просмотра.
/// </summary>
public interface IProgressService
{
    /// <summary>
    /// Получить прогресс просмотра пользователя по эпизоду.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="episodeId">Идентификатор эпизода.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<WatchProgressDto?> GetAsync(Guid userId, Guid episodeId, CancellationToken ct);

    /// <summary>
    /// Создать/обновить прогресс просмотра пользователя по эпизоду.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="episodeId">Идентификатор эпизода.</param>
    /// <param name="req">Позиция просмотра.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<WatchProgressDto> UpsertAsync(Guid userId, Guid episodeId, UpsertProgressRequest req, CancellationToken ct);
}
