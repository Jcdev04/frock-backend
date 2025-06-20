using Frock_backend.network.Domain.Model.Aggregates;

namespace Frock_backend.network.Domain.Model.DTOs
{
    public class CreateRouteDto
    {
        public double Price { get; set; }
        public int Duration { get; set; }
        public int Frequency { get; set; }
        public List<int> Stops { get; set;  }
        public List<Schedule> Schedules { get; set; }
        public CreateRouteDto(double price, int duration, int frequency, List<int> stops, List<Schedule> schedules)
        {
            Price = price;
            Duration = duration;
            Frequency = frequency;
            Stops = stops;
            Schedules = schedules;
        }
    }
}
