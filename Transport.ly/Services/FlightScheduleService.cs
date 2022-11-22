using Transport.ly.Models;

namespace Transport.ly.Services;

public class FlightScheduleService : IFlightScheduleService
{
    public Day[] Schedule { get; private set; }
    
    public FlightScheduleService()
    {
        CreateSchedule();
    }

    public void PrintSchedule()
    {
        Console.WriteLine("Flight Schedule:");
            
        foreach (var day in Schedule)
        {
            foreach (var flight in day.Flights)
            {
                Console.WriteLine($"Flight: {flight.Id}, " +
                                  $"departure: {flight.Departure}, " +
                                  $"arrival: {flight.Arrival}, " +
                                  $"day: {flight.Day}");
            }
        }
    }

    public List<Order> GetOrdersByFlightNumber(string id)
    {
        foreach (var day in Schedule)
        {
            foreach (var flight in day.Flights)
            {
                if (id == flight.Id)
                {
                    return flight.Orders;
                }
            }
        }

        return new List<Order>();
    }

    private void CreateSchedule()
    {
        // Init Planes
        var plane1 = new Plane { Id = "1", Capacity = 20 };
        var plane2 = new Plane { Id = "2", Capacity = 20 };
        var plane3 = new Plane { Id = "3", Capacity = 20 };
            
        // Init Flights
        var day1 = new Day
        {
            Id = "1",
            Number = 1,
            Flights = new List<Flight>
            {
                new Flight { Id = "1", Day = 1, Plane = plane1, Departure = "YUL", Arrival = "YYZ" },
                new Flight { Id = "2", Day = 1, Plane = plane2, Departure = "YUL", Arrival = "YYC" },
                new Flight { Id = "3", Day = 1, Plane = plane3, Departure = "YUL", Arrival = "YVR" }
            }
        };
            
        var day2 = new Day
        {
            Id = "2",
            Number = 2,
            Flights = new List<Flight>
            {
                new Flight { Id = "4", Day = 2, Plane = plane1, Departure = "YUL", Arrival = "YYZ" },
                new Flight { Id = "5", Day = 2, Plane = plane2, Departure = "YUL", Arrival = "YYC" },
                new Flight { Id = "6", Day = 2, Plane = plane3, Departure = "YUL", Arrival = "YVR" }
            }
        };

        Schedule = new[] { day1, day2 };
    }
}