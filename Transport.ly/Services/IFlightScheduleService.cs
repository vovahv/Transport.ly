using Transport.ly.Models;

namespace Transport.ly.Services;

public interface IFlightScheduleService
{
    Day[] Schedule { get; }
    void PrintSchedule();
}