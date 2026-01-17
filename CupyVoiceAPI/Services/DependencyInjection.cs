using CupyVoiceAPI.Services.Interfaces;
using CupyVoiceAPI.Services.Realization;

namespace CupyVoiceAPI.Services;


/// <summary>
/// Регистрация сервисов прикладного слоя.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавить сервисы приложения в DI контейнер.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISeriesService, SeriesService>();
        services.AddScoped<ISeasonsService, SeasonsService>();
        services.AddScoped<IEpisodesService, EpisodesService>();
        services.AddScoped<IProgressService, ProgressService>();
        services.AddScoped<IPlayerPreferencesService, PlayerPreferencesService>();

        return services;
    }
}
