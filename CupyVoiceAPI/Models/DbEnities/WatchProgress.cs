using System.ComponentModel.DataAnnotations.Schema;

namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Прогресс просмотра
/// </summary>
[Table("watch_progress")]
public sealed class WatchProgress
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Id эпизода
    /// </summary>
    public Guid EpisodeId { get; set; }
    
    /// <summary>
    /// Позиция в секундах
    /// </summary>
    public int PositionSeconds { get; set; }
    
    /// <summary>
    /// Время последняго обновления
    /// </summary>
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
}
