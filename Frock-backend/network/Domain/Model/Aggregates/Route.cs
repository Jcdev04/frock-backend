namespace Frock_backend.network.Domain.Model.Aggregates
{
    public class RouteConnection
    {

        public int Id { get; }
        public double Price{ get; set; }
        public int Duration { get; set; }
        public int Frequency { get; set; }

        public RouteConnection(double price, int duration, int frequency)
        {
            this.Price = price;
            this.Duration = duration;
            this.Frequency = frequency;
        }
    }
}
