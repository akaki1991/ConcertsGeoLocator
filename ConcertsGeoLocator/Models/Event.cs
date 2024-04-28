namespace ConcertsGeoLocator.Models;

public class Event
{
    public string? Venue { get; set; }
    public DateTime OpeningDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public ICollection<Concert> Concerts { get; set; } = new List<Concert>();
    public ICollection<string> Artists { get; set; } = new List<string>();
}
