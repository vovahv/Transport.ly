using Transport.ly.Services;

namespace Transport.ly
{
    internal class Program
    {
        private static readonly IFlightScheduleService _flightScheduleService = new FlightScheduleService();

        private static readonly IOrdersScheduleService _ordersScheduleService = new OrdersScheduleService(_flightScheduleService.Schedule);
        
        static void Main(string[] args)
        {
            // USER STORY #1
            _flightScheduleService.PrintSchedule();

            // USER STORY #2
            _ordersScheduleService.LoadOrdersFromJson();
            _ordersScheduleService.ScheduleOrders();
            _ordersScheduleService.PrintScheduledOrders();
            
            Console.ReadKey();
        }
    }
}