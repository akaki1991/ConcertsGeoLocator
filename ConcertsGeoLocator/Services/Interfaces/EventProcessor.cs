using ConcertsGeoLocator.Services.Implementations;
using Microsoft.Extensions.Logging;

namespace ConcertsGeoLocator.Services.Interfaces;

public class EventProcessor(ISpotifyService spotifyService, ILogger<EventProcessor> logger) : IEventProcessor
{
    private readonly ISpotifyService _spotifyService = spotifyService;
    private readonly ILogger<EventProcessor> _logger = logger;

    public async Task ProcessEventsAsync()
    {
		try
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
		catch (Exception ex)
		{
            _logger.LogError("Error occued while executing EventProcessor class ProcessEventsAsync method exception: {ex}", ex.Message);
            Console.WriteLine("Somthing went wrong :(");
        }
    }
}
