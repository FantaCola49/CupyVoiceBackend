namespace CupyVoiceAPI.Models.Exceptions;

/// <summary>
/// Базовое исключение прикладного слоя. Контроллер/мидлварь мапит это в правильный HTTP-код.
/// </summary>
public abstract class AppException : Exception
{
    protected AppException(string message) : base(message) { }
}
