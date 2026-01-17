using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Серии
/// </summary>
[Table("series")]
public sealed class Series
{
    /// <summary>
    /// Id серии
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Название серии
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание серии
    /// </summary>
    [MaxLength(2000)]
    public string? Description { get; set; }

    /// <summary>
    /// Ссылка на постер
    /// </summary>
    [MaxLength(2000)]
    public string? PosterUrl { get; set; }

    /// <summary>
    /// Сизоны
    /// </summary>
    public List<Season> Seasons { get; set; } = new();
}
