using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Models.Responses;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CupyVoiceAPI.Controllers;

/// <summary>
/// Контроллер настроек плеера.
/// </summary>
[ApiController]
[Route("api/users/{userId:guid}/player-preferences")]
public sealed class PlayerPreferencesController : ControllerBase
{
    private readonly IPlayerPreferencesService _prefsService;

    public PlayerPreferencesController(IPlayerPreferencesService prefsService)
    {
        _prefsService = prefsService;
    }

    /// <summary>
    /// Получить настройки плеера (если нет в БД — дефолт).
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PlayerPreferencesDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlayerPreferencesDto>> Get([FromRoute] Guid userId, CancellationToken ct)
        => Ok(await _prefsService.GetAsync(userId, ct));

    /// <summary>
    /// Создать/обновить настройки плеера.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(PlayerPreferencesDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlayerPreferencesDto>> Upsert([FromRoute] Guid userId, [FromBody] UpdatePlayerPreferencesRequest req, CancellationToken ct)
        => Ok(await _prefsService.UpsertAsync(userId, req, ct));
}
