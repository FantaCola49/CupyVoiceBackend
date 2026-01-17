using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CupyVoiceAPI.Controllers;

/// <summary>
/// Контроллер эпизодов.
/// </summary>
[ApiController]
public sealed class EpisodesController : ControllerBase
{
    private readonly IEpisodesService _episodesService;

    public EpisodesController(IEpisodesService episodesService)
    {
        _episodesService = episodesService;
    }

    /// <summary>
    /// Получить эпизоды сезона.
    /// </summary>
    [HttpGet("api/seasons/{seasonId:guid}/episodes")]
    [ProducesResponseType(typeof(IReadOnlyList<EpisodeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<EpisodeDto>>> GetBySeason([FromRoute] Guid seasonId, CancellationToken ct)
        => Ok(await _episodesService.GetBySeasonAsync(seasonId, ct));

    /// <summary>
    /// Создать эпизод для сезона.
    /// </summary>
    [HttpPost("api/seasons/{seasonId:guid}/episodes")]
    [ProducesResponseType(typeof(EpisodeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<EpisodeDto>> Create([FromRoute] Guid seasonId, [FromBody] CreateEpisodeRequest req, CancellationToken ct)
    {
        // Ожидаем сигнатуру сервиса: CreateAsync(Guid seasonId, CreateEpisodeRequest req, ...)
        var dto = await _episodesService.CreateAsync(seasonId, req, ct);
        return CreatedAtAction(nameof(GetBySeason), new { seasonId }, dto);
    }

    /// <summary>
    /// Обновить эпизод.
    /// </summary>
    [HttpPut("api/episodes/{episodeId:guid}")]
    [ProducesResponseType(typeof(EpisodeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EpisodeDto>> Update([FromRoute] Guid episodeId, [FromBody] UpdateEpisodeRequest req, CancellationToken ct)
        => Ok(await _episodesService.UpdateAsync(episodeId, req, ct));

    /// <summary>
    /// Удалить эпизод.
    /// </summary>
    [HttpDelete("api/episodes/{episodeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid episodeId, CancellationToken ct)
    {
        await _episodesService.DeleteAsync(episodeId, ct);
        return NoContent();
    }

    /// <summary>
    /// Получить параметры воспроизведения эпизода (MVP: mp4 url).
    /// </summary>
    [HttpGet("api/episodes/{episodeId:guid}/playback")]
    [ProducesResponseType(typeof(PlaybackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlaybackDto>> Playback([FromRoute] Guid episodeId, CancellationToken ct)
        => Ok(await _episodesService.GetPlaybackAsync(episodeId, ct));
}
