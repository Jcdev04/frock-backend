namespace Frock_backend.network.Domain.Model.Aggregates
{
    public class RouteStop
    {
        public int Id { get; }
        public int FkIdRoute { get; set; }
        public int FkIdStop { get; set; }

        public RouteStop(int fkIdRoute, int fkIdStop)
        {
            FkIdRoute = fkIdRoute;
            FkIdStop = fkIdStop;
        }

        
    }
}
