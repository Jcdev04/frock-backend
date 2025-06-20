using Frock_backend.stops.Domain.Model.Commands;

namespace Frock_backend.stops.Domain.Model.Aggregates
{
    public class Stop
    {
        public int Id { get; }
        public string Name { get;  set; }
        public string? GoogleMapsUrl { get;  set; }
        public string? ImageUrl { get;  set; }
        public string Phone { get;  set; }
        public int FkIdCompany { get;  set; }
        public string Address { get;  set; }
        public string Reference { get;  set; }
        public string FkIdLocality { get;  set; }

        protected Stop()
        {
            Name = string.Empty;
            GoogleMapsUrl = string.Empty;
            ImageUrl = string.Empty;
            Phone = string.Empty;
            FkIdCompany = 0;
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

        public Stop(UpdateStopCommand command)
        {
            Id = command.Id;
            Name = command.Name;
            GoogleMapsUrl = command.GoogleMapsUrl;
            ImageUrl = command.ImageUrl;
            Phone = command.Phone;
            FkIdCompany = command.FkIdCompany;
            Address = command.Address;
            Reference = command.Reference;
            FkIdLocality = command.FkIdLocality;
        }

        public Stop(DeleteStopCommand command)
        {
            Id = command.Id;
            Name = "";
            GoogleMapsUrl = "";
            ImageUrl = "";
            Phone = "";
            FkIdCompany = 0;
            Address = "";
            Reference = "";
            FkIdLocality = "";
        }
    }
}
