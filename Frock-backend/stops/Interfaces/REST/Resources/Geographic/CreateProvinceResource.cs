namespace Frock_backend.stops.Interfaces.REST.Resources.Geographic
{
    public record CreateProvinceResource(
        string Id, // Unique identifier for the province, e.g. "prov-1"
        string Name, 
        string FkIdRegion // Foreign key to the region this province belongs to
        );
}
