using ConcertsGeoLocator.Models;

namespace ConcertsGeoLocator.Services.Shared;

public class GetConcertsByLocationResponse
{
    public string HeaderImageUri { get; set; }
    public string UserLocation { get; set; }
    public string UserLocationGeonameId { get; set; }
    public IEnumerable<Event> Events { get; set; } = new List<Event>();
}
