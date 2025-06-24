using Frock_backend.stops.Domain.Model.Commands.Geographic;

namespace Frock_backend.stops.Domain.Model.Aggregates.Geographic
{
    public class Locality
    {
        public string Id { get; }
        public string Name { get; set; }
        public string FkIdDistrict { get; set; }
        protected Locality()
        {
            Id = string.Empty;
            Name = string.Empty;
            FkIdDistrict = string.Empty;
        }
        public Locality(CreateLocalityCommand command)
        {
            Id = command.Id;
            Name = command.Name;
            FkIdDistrict = command.FkIdDistrict;
        }
    }
}
