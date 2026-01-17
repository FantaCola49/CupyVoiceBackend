namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Настройки пользователя
/// </summary>
public sealed class PlayerPreferences
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Предпочитаемое качество
    /// </summary>
    public string? PreferredQuality { get; set; } = "1080p";
    
    /// <summary>
    /// Скорость проигрывания
    /// </summary>
    public double PlaybackRate { get; set; } = 1.0;
}
