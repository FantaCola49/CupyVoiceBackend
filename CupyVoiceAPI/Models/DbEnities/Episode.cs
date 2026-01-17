using System.ComponentModel.DataAnnotations;

namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Модель эпизода
/// </summary>
public sealed class Episode
{
    /// <summary>
    /// Id эпизода
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Id сизона
    /// </summary>
    [Required]
    public Guid SeasonId { get; set; }

    /// <summary>
    /// Номер эпизода
    /// </summary>
    [Required]
    public int Number { get; set; }

    /// <summary>
    /// Название эпизода
    /// </summary>
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Длительность в секундах
    /// </summary>
    [Required]
    public int DurationSeconds { get; set; }

    /// <summary>
    /// MVP: прямой URL (позже заменю на S3/MinIO key + presigned URL / HLS)
    /// </summary>
    [MaxLength(4000)]
    public string? VideoUrl { get; set; }
}