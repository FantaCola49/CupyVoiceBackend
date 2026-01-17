using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CupyVoiceAPI.Controllers;

/// <summary>
/// Контроллер серий
/// </summary>
[ApiController]
[Route("api/series")]
public class SeriesController : ControllerBase
{
    /// <summary>
    /// Сервис серий
    /// </summary>
    private readonly ISeriesService _seriesService;
    public SeriesController(ISeriesService seriesService)
    {
        _seriesService = seriesService;
    }

    /// <summary>
    /// Получить список сериалов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SeriesListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SeriesListItemDto>>> GetAll(CancellationToken ct)
        => Ok(await _seriesService.GetAllAsync(ct));

    /// <summary>
    /// Получить сериал по id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SeriesDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeriesDetailsDto>> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var item = await _seriesService.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>
    /// Создать сериал.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SeriesDetailsDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<SeriesDetailsDto>> Create([FromBody] CreateSeriesRequest req, CancellationToken ct)
    {
        var dto = await _seriesService.CreateAsync(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Обновить сериал.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SeriesDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeriesDetailsDto>> Update([FromRoute] Guid id, [FromBody] UpdateSeriesRequest req, CancellationToken ct)
        => Ok(await _seriesService.UpdateAsync(id, req, ct));

    /// <summary>
    /// Удалить сериал.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        await _seriesService.DeleteAsync(id, ct);
        return NoContent();
    }
}
