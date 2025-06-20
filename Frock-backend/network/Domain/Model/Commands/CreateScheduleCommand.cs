namespace Frock_backend.network.Domain.Model.Commands
{
    public record CreateScheduleCommand(
            string DayOfWeek, // The day of the week for the schedule (e.g., "Monday", "Tuesday", etc.)
            int FkIdRoute, // This is a foreign key to a Route entity
            string StartTime, // The start time of the schedule in HH:mm format
            string EndTime // The end time of the schedule in HH:mm format
        );
}
