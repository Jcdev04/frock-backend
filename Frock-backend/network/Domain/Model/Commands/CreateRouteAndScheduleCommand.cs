using Frock_backend.network.Domain.Model.Aggregates;

namespace Frock_backend.network.Domain.Model.Commands
{
    public record CreateRouteAndScheduleCommand(string Name, IEnumerable<int> StopIds, IEnumerable<Schedule> Schedules);
}
