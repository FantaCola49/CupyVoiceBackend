using CupyVoiceAPI.Data;
using CupyVoiceAPI.Services;
using Microsoft.AspNetCore.Mvc;
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

        #region Services

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();
        builder.Services.AddApplicationServices(); // регистрирую все рабочие сервисы

        #endregion

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            var cs = builder.Configuration.GetConnectionString("Default")
                     ?? throw new InvalidOperationException("Missing connection string: ConnectionStrings:Default");
            options.UseNpgsql(cs);
        });
        builder.Services.Configure<ApiBehaviorOptions>(o =>
        {
            o.InvalidModelStateResponseFactory = ctx =>
            {
                var pd = new ValidationProblemDetails(ctx.ModelState)
                {
                    Title = "Validation failed",
                    Status = StatusCodes.Status400BadRequest
                };
                return new BadRequestObjectResult(pd);
            };
        });

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("frontend", p =>
            {
                p.WithOrigins("http://localhost:5173")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
            });
        });

        var app = builder.Build();
        app.UseCors("frontend");
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
