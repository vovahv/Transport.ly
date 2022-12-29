namespace Transport.ly.Models;

public class Day
{
    public string Id { get; init; }
    
    public int Number { get; init; }

    private readonly List<Flight> _flights;
    public List<Flight> Flights
    {
        get => _flights;

        init
        {
            _flights = value;
            FlightByDestination = _flights.ToDictionary(f => f.Arrival, f => f);
        }
    }

    public Dictionary<string, Flight> FlightByDestination { get; private init; }
}