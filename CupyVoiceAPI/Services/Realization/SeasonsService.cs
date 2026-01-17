using CupyVoiceAPI.Data;
using CupyVoiceAPI.Models.DbEnities;
using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Exceptions;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupyVoiceAPI.Services.Realization;


/// <summary>
/// Реализация сервиса сезонов на EF Core.
/// </summary>
public sealed class SeasonsService : ISeasonsService
{
    private readonly AppDbContext _db;

    public SeasonsService(AppDbContext db) => _db = db;

    ///<inheritdoc/>
    public async Task<IReadOnlyList<SeasonDto>> GetBySeriesAsync(Guid seriesId, CancellationToken ct)
    {
        var seriesExists = await _db.Series.AsNoTracking().AnyAsync(x => x.Id == seriesId, ct);
        if (!seriesExists) throw new NotFoundException("Series not found.");

        return await _db.Seasons
            .AsNoTracking()
            .Where(x => x.SeriesId == seriesId)
            .OrderBy(x => x.Number)
            .Select(x => new SeasonDto(x.Id, x.SeriesId, x.Number))
            .ToListAsync(ct);
    }

    ///<inheritdoc/>
    public async Task<SeasonDto> CreateAsync(Guid SeriesId, CreateSeasonRequest req, CancellationToken ct)
    {
        var seriesExists = await _db.Series.AsNoTracking().AnyAsync(x => x.Id == SeriesId, ct);
        if (!seriesExists) throw new NotFoundException("Series not found.");

        var duplicate = await _db.Seasons.AsNoTracking()
            .AnyAsync(x => x.SeriesId == SeriesId && x.Number == req.Number, ct);

        if (duplicate) throw new ConflictException("Season with the same number already exists.");

        var entity = new Season
        {
            SeriesId = SeriesId,
            Number = req.Number
        };

        _db.Seasons.Add(entity);
        await _db.SaveChangesAsync(ct);

        return new SeasonDto(entity.Id, entity.SeriesId, entity.Number);
    }

    ///<inheritdoc/>
    public async Task DeleteAsync(Guid seasonId, CancellationToken ct)
    {
        var entity = await _db.Seasons.FirstOrDefaultAsync(x => x.Id == seasonId, ct);
        if (entity is null) throw new NotFoundException("Season not found.");

        _db.Seasons.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
