using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Requests;

namespace CupyVoiceAPI.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса серий
/// </summary>
public interface ISeriesService
{
    /// <summary>
    /// Получить список сериалов (для главной страницы/каталога).
    /// </summary>
    /// <param name="ct">Токен отмены.</param>
    Task<IReadOnlyList<SeriesListItemDto>> GetAllAsync(CancellationToken ct);

    /// <summary>
    /// Получить сериал по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сериала.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<SeriesDetailsDto?> GetByIdAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Создать сериал.
    /// </summary>
    /// <param name="req">Данные для создания сериала.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<SeriesDetailsDto> CreateAsync(CreateSeriesRequest req, CancellationToken ct);

    /// <summary>
    /// Обновить сериал.
    /// </summary>
    /// <param name="id">Идентификатор сериала.</param>
    /// <param name="req">Данные для обновления.</param>
    /// <param name="ct">Токен отмены.</param>
    Task<SeriesDetailsDto> UpdateAsync(Guid id, UpdateSeriesRequest req, CancellationToken ct);

    /// <summary>
    /// Удалить сериал (и связанные сезоны/серии при настроенных каскадах).
    /// </summary>
    /// <param name="id">Идентификатор сериала.</param>
    /// <param name="ct">Токен отмены.</param>
    Task DeleteAsync(Guid id, CancellationToken ct);
}
