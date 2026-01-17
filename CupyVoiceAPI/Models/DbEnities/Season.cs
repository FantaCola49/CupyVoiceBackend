namespace CupyVoiceAPI.Models.DbEnities;

/// <summary>
/// Сезон
/// </summary>
public sealed class Season
{
    /// <summary>
    /// Id сизона
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Id серии
    /// </summary>
    public Guid SeriesId { get; set; }
    
    /// <summary>
    /// Номер сезона
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Эпизоды
    /// </summary>
    public List<Episode> Episodes { get; set; } = new();
}
