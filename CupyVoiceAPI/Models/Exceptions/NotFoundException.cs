namespace CupyVoiceAPI.Models.Exceptions;

/// <summary>
/// Ресурс не найден (404).
/// </summary>
public sealed class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message) { }
}
