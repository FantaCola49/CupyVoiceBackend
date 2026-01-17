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
    public DbSet<WatchProgress> WatchProgress => Set<WatchProgress>();
    public DbSet<PlayerPreferences> PlayerPreferences => Set<PlayerPreferences>();

    /// <summary>
    /// Правила для миграций
    /// </summary>
    /// <param name="modelBuilder">Строитель моделей</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Связи (явно, чтобы не было сюрпризов с каскадами/навигацией).
        modelBuilder.Entity<Series>()
            .HasMany(x => x.Seasons)
            .WithOne()
            .HasForeignKey(x => x.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Season>()
            .HasMany(x => x.Episodes)
            .WithOne()
            .HasForeignKey(x => x.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);

        // Уникальность номера сезона в рамках сериала.
        modelBuilder.Entity<Season>()
            .HasIndex(x => new { x.SeriesId, x.Number })
            .IsUnique();

        // Уникальность номера эпизода в рамках сезона.
        modelBuilder.Entity<Episode>()
            .HasIndex(x => new { x.SeasonId, x.Number })
            .IsUnique();

        // Составной ключ (UserId + EpisodeId).
        modelBuilder.Entity<WatchProgress>(b =>
        {
            b.HasKey(x => new { x.UserId, x.EpisodeId });
            b.HasIndex(x => x.UpdatedAtUtc);
        });
    }


}
