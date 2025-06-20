using Frock_backend.network.Domain.Model.Aggregates;
using Frock_backend.network.Domain.Model.Commands;

namespace Frock_backend.network.Domain.Services
{
    public interface IScheduleCommandService
    {
        Task<Schedule?> Handle(CreateScheduleCommand command);
    }
}
