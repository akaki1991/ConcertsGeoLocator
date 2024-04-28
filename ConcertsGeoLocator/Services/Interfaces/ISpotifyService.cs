using ConcertsGeoLocator.Services.Shared;

namespace ConcertsGeoLocator.Services.Interfaces;

public interface ISpotifyService
{
    Task<GetConcertsByLocationResponse?> GetConcertsByLocationAsync(string location);
}
