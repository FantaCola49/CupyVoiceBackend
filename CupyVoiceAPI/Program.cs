using CupyVoiceAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace CupyVoiceAPI;

/// <summary>
/// Входная точка приложения
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var cs = builder.Configuration.GetConnectionString("Default")
                     ?? throw new InvalidOperationException("Missing connection string: ConnectionStrings:Default");
            options.UseNpgsql(cs);
        });
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
