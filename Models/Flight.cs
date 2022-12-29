namespace Transport.ly.Models;

public class Flight
{
    public string Id { get; init;  }
    public int Day { get; init; }
    public string Departure { get; init; }
    public string Arrival { get; init; }
    public Plane Plane { get; init; }
    public List<Order> Orders { get; init; } = new();

}