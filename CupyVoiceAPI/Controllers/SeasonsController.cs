using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CupyVoiceAPI.Controllers;

/// <summary>
/// Контроллер сезонов.
/// </summary>
[ApiController]
public sealed class SeasonsController : ControllerBase
{
    private readonly ISeasonsService _seasonsService;

    public SeasonsController(ISeasonsService seasonsService)
    {
        _seasonsService = seasonsService;
    }

    /// <summary>
    /// Получить сезоны сериала.
    /// </summary>
    [HttpGet("api/series/{seriesId:guid}/seasons")]
    [ProducesResponseType(typeof(IReadOnlyList<SeasonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<SeasonDto>>> GetBySeries([FromRoute] Guid seriesId, CancellationToken ct)
        => Ok(await _seasonsService.GetBySeriesAsync(seriesId, ct));

    /// <summary>
    /// Создать сезон для сериала.
    /// </summary>
    [HttpPost("api/series/{seriesId:guid}/seasons")]
    [ProducesResponseType(typeof(SeasonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SeasonDto>> Create([FromRoute] Guid seriesId, [FromBody] CreateSeasonRequest req, CancellationToken ct)
    {
        // Ожидаем сигнатуру сервиса: CreateAsync(Guid seriesId, CreateSeasonRequest req, ...)
        var dto = await _seasonsService.CreateAsync(seriesId, req, ct);
        return CreatedAtAction(nameof(GetBySeries), new { seriesId }, dto);
    }

    /// <summary>
    /// Удалить сезон.
    /// </summary>
    [HttpDelete("api/seasons/{seasonId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid seasonId, CancellationToken ct)
    {
        await _seasonsService.DeleteAsync(seasonId, ct);
        return NoContent();
    }
}
