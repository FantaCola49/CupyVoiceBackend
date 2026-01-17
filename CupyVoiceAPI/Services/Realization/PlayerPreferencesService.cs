using CupyVoiceAPI.Data;
using CupyVoiceAPI.Models.DbEnities;
using CupyVoiceAPI.Models.Requests;
using CupyVoiceAPI.Models.Responses;
using CupyVoiceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CupyVoiceAPI.Services.Realization;


/// <summary>
/// Реализация сервиса настроек плеера на EF Core.
/// </summary>
public sealed class PlayerPreferencesService : IPlayerPreferencesService
{
    private readonly AppDbContext _db;

    public PlayerPreferencesService(AppDbContext db) => _db = db;

    ///<inheritdoc/>
    public async Task<PlayerPreferencesDto> GetAsync(Guid userId, CancellationToken ct)
    {
        var dto = await _db.PlayerPreferences
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new PlayerPreferencesDto(x.UserId, x.PreferredQuality, x.PlaybackRate))
            .FirstOrDefaultAsync(ct);

        return dto ?? new PlayerPreferencesDto(userId, "1080p", 1.0);
    }

    ///<inheritdoc/>
    public async Task<PlayerPreferencesDto> UpsertAsync(Guid userId, UpdatePlayerPreferencesRequest req, CancellationToken ct)
    {
        var entity = await _db.PlayerPreferences.FirstOrDefaultAsync(x => x.UserId == userId, ct);

        if (entity is null)
        {
            entity = new PlayerPreferences
            {
                UserId = userId,
                PreferredQuality = string.IsNullOrWhiteSpace(req.PreferredQuality) ? "1080p" : req.PreferredQuality.Trim(),
                PlaybackRate = req.PlaybackRate
            };
            _db.PlayerPreferences.Add(entity);
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(req.PreferredQuality))
                entity.PreferredQuality = req.PreferredQuality.Trim();

            entity.PlaybackRate = req.PlaybackRate;
        }

        await _db.SaveChangesAsync(ct);

        return new PlayerPreferencesDto(entity.UserId, entity.PreferredQuality, entity.PlaybackRate);
    }
}
