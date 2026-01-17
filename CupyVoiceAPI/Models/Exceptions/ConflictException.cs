namespace CupyVoiceAPI.Models.Exceptions;

/// <summary>
/// Конфликт состояния (409) — например, дубль, некорректное состояние сущности.
/// </summary>
public sealed class ConflictException : AppException
{
    public ConflictException(string message) : base(message) { }
}
