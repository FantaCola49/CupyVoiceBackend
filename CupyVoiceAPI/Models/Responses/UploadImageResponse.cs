namespace CupyVoiceAPI.Models.Responses;

/// <summary>
/// Ответ на запрос по загрузке картинки
/// </summary>
/// <param name="Url"></param>
public sealed record UploadImageResponse(string Url);