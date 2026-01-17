using CupyVoiceAPI.Models.DbEnities;
using Microsoft.EntityFrameworkCore;

namespace CupyVoiceAPI.Data;

/// <summary>
/// Контекст подключения к базе данных
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Series> Series => Set<Series>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Episode> Episodes => Set<Episode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Если у тебя уже есть DataAnnotations — можешь оставить пустым.
        // Иначе опиши ключи/индексы/ограничения здесь.
        base.OnModelCreating(modelBuilder);
    }

}
