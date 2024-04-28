using ConcertsGeoLocator.Configuration;
using ConcertsGeoLocator.Services.Implementations;
using ConcertsGeoLocator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertsGeoLocator;

public class DefaultDependencyResolver
{
    private IConfiguration _configurationRoot;

    public IServiceCollection RegisterServices(IServiceCollection? services = null)
    {
        services ??= new ServiceCollection();

        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        services.Configure<SpotifyApiConfiguration>(options => _configurationRoot.GetSection(nameof(SpotifyApiConfiguration)).Bind(options));

        services.AddHttpClient();
        services.AddSingleton<ISpotifyService, SpotifyService>();
        services.AddSingleton<IEventProcessor, EventProcessor>();

        return services;
    }
}
