namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Модель эпизода
/// </summary>
public sealed class Episode
{
    /// <summary>
    /// Id эпизода
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Id сизона
    /// </summary>
    public Guid SeasonId { get; set; }
    
    /// <summary>
    /// Номер эпизода
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Название эпизода
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Длительность в секундах
    /// </summary>
    public int DurationSeconds { get; set; }

    /// <summary>
    /// MVP: прямой URL (позже заменю на S3/MinIO key + presigned URL / HLS)
    /// </summary>
    public string? VideoUrl { get; set; }
}