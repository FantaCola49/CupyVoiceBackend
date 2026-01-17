using CupyVoiceAPI.Data;
using CupyVoiceAPI.Models.DbEnities;
using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Exceptions;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupyVoiceAPI.Services.Realization;


/// <summary>
/// Реализация сервиса сериалов на EF Core.
/// </summary>
public sealed class SeriesService : ISeriesService
{
    private readonly AppDbContext _db;

    public SeriesService(AppDbContext db) => _db = db;

    ///<inheritdoc/>
    public async Task<IReadOnlyList<SeriesListItemDto>> GetAllAsync(CancellationToken ct)
        => await _db.Series
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .Select(x => new SeriesListItemDto(x.Id, x.Title, x.Description, x.PosterUrl))
            .ToListAsync(ct);

    ///<inheritdoc/>
    public async Task<SeriesDetailsDto?> GetByIdAsync(Guid id, CancellationToken ct)
        => await _db.Series
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new SeriesDetailsDto(x.Id, x.Title, x.Description, x.PosterUrl))
            .FirstOrDefaultAsync(ct);

    ///<inheritdoc/>
    public async Task<SeriesDetailsDto> CreateAsync(CreateSeriesRequest req, CancellationToken ct)
    {
        var entity = new Series
        {
            Title = req.Title.Trim(),
            Description = req.Description?.Trim(),
            PosterUrl = req.PosterUrl?.Trim()
        };

        _db.Series.Add(entity);
        await _db.SaveChangesAsync(ct);

        return new SeriesDetailsDto(entity.Id, entity.Title, entity.Description, entity.PosterUrl);
    }

    ///<inheritdoc/>
    public async Task<SeriesDetailsDto> UpdateAsync(Guid id, UpdateSeriesRequest req, CancellationToken ct)
    {
        var entity = await _db.Series.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) throw new NotFoundException("Series not found.");

        entity.Title = req.Title.Trim();
        entity.Description = req.Description?.Trim();
        entity.PosterUrl = req.PosterUrl?.Trim();

        await _db.SaveChangesAsync(ct);

        return new SeriesDetailsDto(entity.Id, entity.Title, entity.Description, entity.PosterUrl);
    }

    ///<inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Series.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) throw new NotFoundException("Series not found.");

        _db.Series.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
