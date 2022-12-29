namespace Transport.ly.Services;

public interface IOrdersScheduleService
{
    void LoadOrdersFromJson();
    void ScheduleOrders();
    void PrintScheduledOrders();
}