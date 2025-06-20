namespace Frock_backend.network.Domain.Model.Aggregates
{
    public class Schedule
    {
        public int Id { get; }
        public int FkIdRoute { get; set; }
        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public Schedule(int fkIdRoute, string dayOfWeek, string startTime, string endTime)
        {
            FkIdRoute = fkIdRoute;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }


    }
}
