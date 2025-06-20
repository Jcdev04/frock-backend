using Frock_backend.stops.Domain.Model.Commands.Geographic;

namespace Frock_backend.stops.Domain.Model.Aggregates.Geographic
{
    public class Province
    {
        public string Id { get; }
        public string Name { get; set; }
        public string FkIdRegion { get; set; }

        protected Province()
        { 
            Id = string.Empty;
            Name = string.Empty;
            FkIdRegion = string.Empty;
        }

        public Province(CreateProvinceCommand command)
        {
            Id = command.Id;
            Name = command.Name;
            FkIdRegion = command.FkIdRegion;
        }
    }
}
