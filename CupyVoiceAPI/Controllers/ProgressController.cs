using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CupyVoiceAPI.Controllers;

/// <summary>
/// Контроллер прогресса просмотра.
/// </summary>
[ApiController]
[Route("api/users/{userId:guid}/episodes/{episodeId:guid}/progress")]
public sealed class ProgressController : ControllerBase
{
    private readonly IProgressService _progressService;

    public ProgressController(IProgressService progressService)
    {
        _progressService = progressService;
    }

    /// <summary>
    /// Получить прогресс просмотра.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(WatchProgressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WatchProgressDto>> Get([FromRoute] Guid userId, [FromRoute] Guid episodeId, CancellationToken ct)
    {
        var dto = await _progressService.GetAsync(userId, episodeId, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    /// <summary>
    /// Создать/обновить прогресс просмотра.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(WatchProgressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WatchProgressDto>> Upsert(
        [FromRoute] Guid userId,
        [FromRoute] Guid episodeId,
        [FromBody] UpsertProgressRequest req,
        CancellationToken ct)
        => Ok(await _progressService.UpsertAsync(userId, episodeId, req, ct));
}
