namespace Frock_backend.stops.Interfaces.REST.Resources.Geographic
{
    public record CreateLocalityResource(
        string Id, // Unique identifier for the locality, e.g. "loc-1"
        string Name, // Official name of the locality
        string FkIdDistrict // Foreign key to the District entity (e.g., "dis-1" for the district this locality belongs to)
        );
}
