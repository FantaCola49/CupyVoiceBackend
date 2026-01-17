using CupyVoiceAPI.Data;
using CupyVoiceAPI.Models.DbEnities;
using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Exceptions;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupyVoiceAPI.Services.Realization;


/// <summary>
/// Реализация сервиса эпизодов на EF Core.
/// </summary>
public sealed class EpisodesService : IEpisodesService
{
    private readonly AppDbContext _db;

    public EpisodesService(AppDbContext db) => _db = db;

    ///<inheritdoc/>
    public async Task<IReadOnlyList<EpisodeDto>> GetBySeasonAsync(Guid seasonId, CancellationToken ct)
    {
        var seasonExists = await _db.Seasons.AsNoTracking().AnyAsync(x => x.Id == seasonId, ct);
        if (!seasonExists) throw new NotFoundException("Season not found.");

        return await _db.Episodes
            .AsNoTracking()
            .Where(x => x.SeasonId == seasonId)
            .OrderBy(x => x.Number)
            .Select(x => new EpisodeDto(x.Id, x.SeasonId, x.Number, x.Title, x.DurationSeconds, x.VideoUrl))
            .ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<EpisodeDto> CreateAsync(Guid seasonId, CreateEpisodeRequest req, CancellationToken ct)
    {
        var seasonExists = await _db.Seasons.AsNoTracking().AnyAsync(x => x.Id == seasonId, ct);
        if (!seasonExists) throw new NotFoundException("Season not found.");

        var duplicate = await _db.Episodes.AsNoTracking()
            .AnyAsync(x => x.SeasonId == seasonId && x.Number == req.Number, ct);

        if (duplicate) throw new ConflictException("Episode with the same number already exists.");

        var entity = new Episode
        {
            SeasonId = seasonId,
            Number = req.Number,
            Title = req.Title.Trim(),
            DurationSeconds = req.DurationSeconds,
            VideoUrl = req.VideoUrl?.Trim()
        };

        _db.Episodes.Add(entity);
        await _db.SaveChangesAsync(ct);

        return new EpisodeDto(entity.Id, entity.SeasonId, entity.Number, entity.Title, entity.DurationSeconds, entity.VideoUrl);
    }

    ///<inheritdoc/>
    public async Task<EpisodeDto> UpdateAsync(Guid episodeId, UpdateEpisodeRequest req, CancellationToken ct)
    {
        var entity = await _db.Episodes.FirstOrDefaultAsync(x => x.Id == episodeId, ct);
        if (entity is null) throw new NotFoundException("Episode not found.");

        entity.Title = req.Title.Trim();
        entity.DurationSeconds = req.DurationSeconds;
        entity.VideoUrl = req.VideoUrl?.Trim();

        await _db.SaveChangesAsync(ct);

        return new EpisodeDto(entity.Id, entity.SeasonId, entity.Number, entity.Title, entity.DurationSeconds, entity.VideoUrl);
    }

    ///<inheritdoc/>
    public async Task<PlaybackDto> GetPlaybackAsync(Guid episodeId, CancellationToken ct)
    {
        var entity = await _db.Episodes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == episodeId, ct);
        if (entity is null) throw new NotFoundException("Episode not found.");

        if (string.IsNullOrWhiteSpace(entity.VideoUrl))
            throw new ConflictException("Episode has no VideoUrl yet.");

        return new PlaybackDto("mp4", entity.VideoUrl);
    }

    ///<inheritdoc/>
    public async Task DeleteAsync(Guid episodeId, CancellationToken ct)
    {
        var entity = await _db.Episodes.FirstOrDefaultAsync(x => x.Id == episodeId, ct);
        if (entity is null) throw new NotFoundException("Episode not found.");

        _db.Episodes.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}
