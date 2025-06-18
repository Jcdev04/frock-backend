namespace Frock_backend.stops.Interfaces.REST.Resources.Geographic
{
    public record LocalityResource(
        string Id,
        string Name,
        string FkIdDistrict // Foreign key to the District entity (e.g., "dis-1" for the district this locality belongs to)
        );
}
