namespace Transport.ly.Models;

public class ScheduledOrder
{
    public string Id { get; init; }
    public Order Order { get; init; }
    public Flight Flight { get; init; }
}