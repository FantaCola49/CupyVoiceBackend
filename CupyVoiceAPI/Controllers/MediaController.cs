using CupyVoiceAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CupyVoiceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MediaController : ControllerBase
{
    private readonly HashSet<string> AllowedTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/png",
        "image/jpeg",
        "image/webp"
    };

    [HttpPost("images")]
    [RequestSizeLimit(10_000_000)] // 10 MB
    [ProducesResponseType(typeof(UploadImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UploadImageResponse>> UploadImage([FromForm] IFormFile file, CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            return BadRequest("File is required.");

        if (!AllowedTypes.Contains(file.ContentType))
            return BadRequest("Only png/jpg/webp allowed.");

        var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsDir);

        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(ext)) ext = file.ContentType switch
        {
            "image/png" => ".png",
            "image/jpeg" => ".jpg",
            "image/webp" => ".webp",
            _ => ".bin"
        };

        var name = $"{Guid.NewGuid():N}{ext}";
        var path = Path.Combine(uploadsDir, name);

        await using (var stream = System.IO.File.Create(path))
        {
            await file.CopyToAsync(stream, ct);
        }

        var url = $"/uploads/{name}";
        return Ok(new UploadImageResponse(url));
    }
}