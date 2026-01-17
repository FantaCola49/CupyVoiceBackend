using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Requests;

namespace CupyVoiceAPI.Services.Interfaces;

/// <summary>
/// Сервис управления сезонами.
/// </summary>
public interface ISeasonsService
{
    /// <summary>
    /// Получить сезоны сериала.
    /// </summary>
    /// <param name="seriesId">Идентификатор сериала.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<IReadOnlyList<SeasonDto>> GetBySeriesAsync(Guid seriesId, CancellationToken ct);

    /// <summary>
    /// Создать сезон сериала.
    /// </summary>
    /// <param name="SeriesId">Id серии.</param>
    /// <param name="req">Данные для создания сезона.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<SeasonDto> CreateAsync(Guid SeriesId, CreateSeasonRequest req, CancellationToken ct);

    /// <summary>
    /// Удалить сезон.
    /// </summary>
    /// <param name="seasonId">Идентификатор сезона.</param>
    /// <param name="ct">Токен отмены.</param>
    Task DeleteAsync(Guid seasonId, CancellationToken ct);
}