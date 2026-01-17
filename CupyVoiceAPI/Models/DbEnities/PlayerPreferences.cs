using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Настройки пользователя
/// </summary>
[Table("player_preferences")]
public sealed class PlayerPreferences
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    [Key]
    public Guid UserId { get; set; }

    /// <summary>
    /// Предпочитаемое качество
    /// </summary>
    [MaxLength(50)]
    public string? PreferredQuality { get; set; } = "1080p";
    
    /// <summary>
    /// Скорость проигрывания
    /// </summary>
    public double PlaybackRate { get; set; } = 1.0;
}
