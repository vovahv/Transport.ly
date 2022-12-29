using System.Text.Json;
using Transport.ly.Models;

namespace Transport.ly.Services;

public class OrdersScheduleService : IOrdersScheduleService
{
    private readonly Day[] _flightSchedule;

    private Order[] _orders;
    private List<Order> _notScheduledOrders;

    private readonly Dictionary<string, int> PriorityByService = new Dictionary<string, int>
    {
        {"same-day", 1},
        {"next-day", 2},
        {"regular", 3},
    };

    public OrdersScheduleService(Day[] flightSchedule)
    {
        _flightSchedule = flightSchedule;
    }

    public void LoadOrdersFromJson()
    {
        var json = File.ReadAllText(@"./coding-assigment-orders.json");
        var ordersConfig = JsonSerializer.Deserialize<Dictionary<string, OrderConfig>>(json);
        
        _orders = ordersConfig
            .Select((p, i) => new Order
            {
                Id = p.Key, 
                Priority = i + 1, 
                Destination = p.Value.destination,
                Service = p.Value.service
            })
            .ToArray();
    }

    public void ScheduleOrders()
    {
        _notScheduledOrders = new List<Order>();

        var orderedOrders = _orders.OrderBy(o => PriorityByService[o.Service]).ToList();
            
        foreach (var order in orderedOrders)
        {
            var orderWasScheduled = false;
            foreach (var day in _flightSchedule)
            {
                var flightWasFound = day.FlightByDestination.TryGetValue(order.Destination, out var flight);
                if (flightWasFound && flight?.Orders.Count < flight?.Plane.Capacity)
                {
                    flight.Orders.Add(order);
                    orderWasScheduled = true;
                    break;
                }
            }

            if (!orderWasScheduled)
            {
                _notScheduledOrders.Add(order);
            }
        }
    }

    public void PrintScheduledOrders()
    {
        Console.WriteLine("\nScheduled Orders:");
            
        var scheduledOrders = _flightSchedule.SelectMany(day =>
                day.Flights.SelectMany(flight =>
                    flight.Orders.Select(
                        order => new ScheduledOrder {Id = order.Id, Order = order, Flight = flight})))
            .ToList();

        foreach (var order in scheduledOrders)
        {
            Console.WriteLine($"order: {order.Id}, " +
                              $"flightNumber: {order.Flight.Id}, " +
                              $"departure: {order.Flight.Departure}, " +
                              $"arrival: {order.Flight.Arrival}, " +
                              $"day: {order.Flight.Day}");
        }

        foreach (var order in _notScheduledOrders)
        {
            Console.WriteLine($"order: {order.Id}, flightNumber: not scheduled");
        }
    }
}