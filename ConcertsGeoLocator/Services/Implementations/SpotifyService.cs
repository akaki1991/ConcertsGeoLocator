using ConcertsGeoLocator.Configuration;
using ConcertsGeoLocator.Services.Interfaces;
using ConcertsGeoLocator.Services.Shared;
using ConcertsGeoLocator.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConcertsGeoLocator.Services.Implementations;

public class SpotifyService(IHttpClientFactory httpClientFactory, IOptions<SpotifyApiConfiguration> options, ILogger<SpotifyService> logger) : ISpotifyService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<SpotifyService> _logger = logger;
    private readonly SpotifyApiConfiguration _configuration = options.Value;

    public async Task<GetConcertsByLocationResponse?> GetConcertsByLocationAsync(string location)
    {
		try
		{
            using var httpclient = _httpClientFactory.CreateClient();

            httpclient.DefaultRequestHeaders.Add(_configuration.ApiKeyName, _configuration.ApiKey);
            var response = await httpclient.GetAsync($"{_configuration.Url}{_configuration.ConcertsPath}/?gl={location}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return Serializer.As<GetConcertsByLocationResponse>(content);
        }
		catch (Exception ex)
		{
            _logger.LogError("Error occued while executing SpotifyService class GetConcertsByLocationAsync method exception: {ex}", ex.Message);
            return default;
		}
    }
}
