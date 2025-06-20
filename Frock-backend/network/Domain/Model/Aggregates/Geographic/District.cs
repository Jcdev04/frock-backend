using Frock_backend.stops.Domain.Model.Commands.Geographic;

namespace Frock_backend.stops.Domain.Model.Aggregates.Geographic
{
    public class District
    {
        public string Id { get; }
        public string Name { get; set; }
        public string FkIdProvince { get; set; }
        protected District()
        {
            Id = string.Empty;
            Name = string.Empty;
            FkIdProvince = string.Empty;
        }
        public District(CreateDistrictCommand command)
        {
            Id = command.Id;
            Name = command.Name;
            FkIdProvince = command.FkIdProvince;
        }
    }
}
