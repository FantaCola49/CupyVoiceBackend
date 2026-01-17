using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Сизон
/// </summary>
[Table("seasons")]
public sealed class Season
{
    /// <summary>
    /// Id сизона
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Id серии
    /// </summary>
    [Required]
    public Guid SeriesId { get; set; }

    /// <summary>
    /// Номер сизона
    /// </summary>
    [Required]
    public int Number { get; set; }

    /// <summary>
    /// Эпизоды
    /// </summary>
    public List<Episode> Episodes { get; set; } = new();
}
