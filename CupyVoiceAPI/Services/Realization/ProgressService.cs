using CupyVoiceAPI.Data;
using CupyVoiceAPI.Models.DbEnities;
using CupyVoiceAPI.Models.Dto;
using CupyVoiceAPI.Models.Exceptions;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupyVoiceAPI.Services.Realization;


/// <summary>
/// Реализация сервиса прогресса просмотра на EF Core.
/// </summary>
public sealed class ProgressService : IProgressService
{
    private readonly AppDbContext _db;

    public ProgressService(AppDbContext db) => _db = db;

    ///<inheritdoc/>
    public async Task<WatchProgressDto?> GetAsync(Guid userId, Guid episodeId, CancellationToken ct)
        => await _db.WatchProgress
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.EpisodeId == episodeId)
            .Select(x => new WatchProgressDto(x.UserId, x.EpisodeId, x.PositionSeconds, x.UpdatedAtUtc))
            .FirstOrDefaultAsync(ct);

    ///<inheritdoc/>
    public async Task<WatchProgressDto> UpsertAsync(Guid userId, Guid episodeId, UpsertProgressRequest req, CancellationToken ct)
    {
        var episodeExists = await _db.Episodes.AsNoTracking().AnyAsync(x => x.Id == episodeId, ct);
        if (!episodeExists) throw new NotFoundException("Episode not found.");

        var entity = await _db.WatchProgress.FirstOrDefaultAsync(x => x.UserId == userId && x.EpisodeId == episodeId, ct);

        if (entity is null)
        {
            entity = new WatchProgress
            {
                UserId = userId,
                EpisodeId = episodeId,
                PositionSeconds = req.PositionSeconds,
                UpdatedAtUtc = DateTime.UtcNow
            };
            _db.WatchProgress.Add(entity);
        }
        else
        {
            entity.PositionSeconds = req.PositionSeconds;
            entity.UpdatedAtUtc = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync(ct);

        return new WatchProgressDto(entity.UserId, entity.EpisodeId, entity.PositionSeconds, entity.UpdatedAtUtc);
    }
}
