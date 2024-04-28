using ConcertsGeoLocator.Services.Implementations;

namespace ConcertsGeoLocator.Services.Interfaces;

public class EventProcessor(ISpotifyService spotifyService) : IEventProcessor
{
    private readonly ISpotifyService _spotifyService = spotifyService;

    public async Task ProcessEventsAsync()
    {
        var response = await _spotifyService.GetConcertsByLocationAsync("US");
        var events = response.Events;

        var oneDayEvents = events.Where(e => e.OpeningDate.Date == e.ClosingDate.Date).ToList();

        var sortedEvents = oneDayEvents.OrderBy(e => e.OpeningDate).ThenBy(e => e.Venue).ToList();

        // Print the event details
        foreach (var eventItem in sortedEvents)
        {
            //Console.WriteLine($"Title: {eventItem.Concerts.}");
            Console.WriteLine($"Date: {eventItem.OpeningDate.ToShortDateString()}");
            Console.WriteLine($"Venue: {eventItem.Venue}");
            Console.WriteLine($"Number of Artists: {eventItem.Artists.Count}");
            Console.WriteLine($"Number of Concerts: {eventItem.Concerts.Count}");
            Console.WriteLine();  // Add a blank line for better readability between events
        }
    }
}
