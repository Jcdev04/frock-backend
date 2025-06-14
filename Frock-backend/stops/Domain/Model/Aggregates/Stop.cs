using Frock_backend.stops.Domain.Model.Commands;

namespace Frock_backend.stops.Domain.Model.Aggregates
{
    public class Stop
    {
        public int Id { get; }
        public string Name { get; private set; }
        public string GoogleMapsUrl { get; private set; }
        public string ImageUrl { get; private set; }
        public string Phone { get; private set; }
        public string FkIdCompany { get; private set; }
        public string Address { get; private set; }
        public string Reference { get; private set; }
        public string FkIdLocality { get; private set; }

        protected Stop()
        {
            Name = string.Empty;
            GoogleMapsUrl = string.Empty;
            ImageUrl = string.Empty;
            Phone = string.Empty;
            FkIdCompany = string.Empty;
            Address = string.Empty;
            Reference = string.Empty;
            FkIdLocality = string.Empty;
        }

        public Stop(CreateStopCommand command)
        {
            Name = command.Name;
            GoogleMapsUrl = command.GoogleMapsUrl;
            ImageUrl = command.ImageUrl;
            Phone = command.Phone;
            FkIdCompany = command.FkIdCompany;
            Address = command.Address;
            Reference = command.Reference;
            FkIdLocality = command.FkIdLocality;
        }
    }
}
