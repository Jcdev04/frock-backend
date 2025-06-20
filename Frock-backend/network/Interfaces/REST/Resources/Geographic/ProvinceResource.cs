namespace Frock_backend.stops.Interfaces.REST.Resources.Geographic
{
    public record ProvinceResource(
        string Id, // Unique identifier for the province (e.g., "prov-1")
        string Name, // Official name of the province
        string FkIdRegion // Foreign key to the Region entity (e.g., "reg-1" for the region this province belongs to)
        );
}
