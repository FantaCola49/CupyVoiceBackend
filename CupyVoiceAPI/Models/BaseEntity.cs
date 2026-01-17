namespace CupyVoiceAPI.Models;

public class BaseEntity
{
    /// <summary>
    /// Id сущности
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}
