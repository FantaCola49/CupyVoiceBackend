using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Models.Responses;

namespace CupyVoiceAPI.Services.Interfaces;

/// <summary>
/// Сервис настроек плеера пользователя.
/// </summary>
public interface IPlayerPreferencesService
{
    /// <summary>
    /// Получить настройки плеера (если в БД нет — вернуть дефолт).
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<PlayerPreferencesDto> GetAsync(Guid userId, CancellationToken ct);

    /// <summary>
    /// Создать/обновить настройки плеера.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="req">Настройки.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<PlayerPreferencesDto> UpsertAsync(Guid userId, UpdatePlayerPreferencesRequest req, CancellationToken ct);
}
