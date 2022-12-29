namespace Transport.ly.Models;

public class Order
{
    public string Id { get; init; }
    public int Priority { get; init; }
    public string Destination { get; init; }
    public string Service { get; init; }
}